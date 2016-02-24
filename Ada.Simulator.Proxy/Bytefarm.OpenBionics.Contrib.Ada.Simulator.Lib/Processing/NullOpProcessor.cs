using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.Processing
{
    public class NullOpProcessor<TInputProtocol> : ProcessorBase<TInputProtocol, TInputProtocol> where TInputProtocol : struct, IProtocolMessage
    {
        public NullOpProcessor(IInput<TInputProtocol> input) : base(input)
        {
        }

        public override TInputProtocol ProcessInput(TInputProtocol input)
        {
            return input;
        }
    }
}