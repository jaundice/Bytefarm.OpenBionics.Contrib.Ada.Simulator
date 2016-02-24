using System.Runtime.Serialization;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO
{
    [DataContract]
    public struct DummyProtocolMessage : IProtocolMessage
    {
        [DataMember]
        public byte Sync0 { get; }

        [DataMember]
        public byte Sync1 { get; }

        [DataMember]
        public uint HandIdentifier { get; }

        [DataMember]
        public ulong SequenceId { get; }

        [DataMember]
        public ulong Timestamp { get; }

        public DummyProtocolMessage(byte sync0, byte sync1, uint handIdentifier, ulong sequenceId, ulong timestamp)
        {
            Sync0 = sync0;
            Sync1 = sync1;
            HandIdentifier = handIdentifier;
            SequenceId = sequenceId;
            Timestamp = timestamp;
        }
    }
}