using System.Runtime.Serialization;

namespace MetaweatherComTests.Entities
{
    [DataContract]
    public class RealGeoData
    {
        [DataMember(Name = "city")] public string City { get; set; }
    }
}