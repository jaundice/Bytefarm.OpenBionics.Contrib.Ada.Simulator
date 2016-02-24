using System.Runtime.Serialization;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Common
{
    [DataContract]
    public struct Motor
    {
        [DataMember]
        public double ExtensionLength;
    }
}
