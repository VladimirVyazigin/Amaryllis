using System.Runtime.Serialization;

namespace MetaweatherComTests.Entities
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "title")] public string Title { get; set; }

        [DataMember(Name = "location_type")] public string LocationType { get; set; }

        [DataMember(Name = "latt_long")] public string LatitudeAndLongitude { get; set; }

        [DataMember(Name = "woeid")] public int WhereOnEarthId { get; set; }

        [DataMember(Name = "distance")] public int Distance { get; set; }

        public string Latitude => LatitudeAndLongitude?.Split(',')[0];

        public string Longitude => LatitudeAndLongitude?.Split(',')[1];
    }
}