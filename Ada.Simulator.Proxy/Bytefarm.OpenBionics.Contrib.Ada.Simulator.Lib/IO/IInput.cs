using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO
{
    public interface IInput
    {
        ListeningState ListeningState { get; }

        void StartListening();
        void StopListening();
        Type InputMessageType { get; }
    }


    public interface IInput<TProtocolMessage> : IInput where TProtocolMessage : struct, IProtocolMessage
    {
        event EventHandler<ProtocolMessageEventArgs<TProtocolMessage>> MessageReceived;
    }
}