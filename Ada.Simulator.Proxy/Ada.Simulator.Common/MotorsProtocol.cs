using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;
using System.Runtime.Serialization;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Common
{
    [DataContract]
    public struct MotorsProtocol : IProtocolMessage
    {
        [DataMember]
        public uint HandIdentifier
        {
            get; set;
        }

        [DataMember]
        public ulong SequenceId
        {
            get; set;
        }

        [DataMember]
        public byte Sync0
        {
            get; set;
        }

        [DataMember]
        public byte Sync1
        {
            get; set;
        }

        [DataMember]
        public Motor[] Motors { get; set; }
    }
}
