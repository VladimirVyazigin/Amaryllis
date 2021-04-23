using System.Runtime.Serialization;

namespace MetaweatherComTests.Entities
{
    [DataContract]
    public class LocationForecast
    {
        [DataMember(Name = "consolidated_weather")]
        public WeatherInfo[] ConsolidatedWeather { get; set; }
    }
}