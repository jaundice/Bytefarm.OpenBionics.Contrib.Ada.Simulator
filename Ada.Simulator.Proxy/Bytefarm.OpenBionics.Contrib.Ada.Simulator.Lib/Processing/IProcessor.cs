using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.Processing
{
    public interface IProcessor
    {
        object Input { get; }
        IProtocolMessage CurrentInput { get; }
        IProtocolMessage CurrentOutput { get; }

        object ProcessInput(object input);

        Type InputMessageType { get; }
        Type OutputMessageType { get; }
    }

    public interface IProcessor<TInputProtocol, TOutputProtocol> : IProcessor
        where TInputProtocol : struct, IProtocolMessage
        where TOutputProtocol : struct, IProtocolMessage
    {
        new IInput<TInputProtocol> Input { get; }
        new TInputProtocol CurrentInput { get; }
        new TOutputProtocol CurrentOutput { get; }

        TOutputProtocol ProcessInput(TInputProtocol input);
    }
}