namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol
{
    public interface IProtocolMessage
    {
        byte Sync0 { get; }
        byte Sync1 { get; }
        uint HandIdentifier { get; }
        ulong SequenceId { get; }
    }
}