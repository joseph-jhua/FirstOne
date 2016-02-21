using System.Runtime.Serialization;

namespace SimpleVehicleSystem.VehicleDataContract
{
    [DataContract]
    public class Vehicle
    {
        [DataMember]
        public long VId { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public string Colour { get; set; }

        [DataMember]
        public int ProduceYear { get; set; }
    }
}
