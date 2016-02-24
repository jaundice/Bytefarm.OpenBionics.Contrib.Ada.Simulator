using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO
{
    public class ProtocolMessageEventArgs<TProtocolMessage> : EventArgs where TProtocolMessage : struct, IProtocolMessage
    {
        public TProtocolMessage Message { get; set; }
    }
}