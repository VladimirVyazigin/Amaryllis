using System;
using System.Runtime.Serialization;

namespace MetaweatherComTests.Entities
{
    [DataContract]
    public class WeatherInfo
    {
        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "weather_state_name")]
        public string WeatherStateName { get; set; }

        [DataMember(Name = "weather_state_abbr")]
        public string WeatherStateAbbr { get; set; }

        [DataMember(Name = "wind_direction_compass")]
        public string WindDirectionCompass { get; set; }

        [DataMember(Name = "created")] public string Created { get; set; }

        [DataMember(Name = "applicable_date")] public string ApplicableDate { get; set; }

        [DataMember(Name = "min_temp")] public float? MinTemp { get; set; }

        [DataMember(Name = "max_temp")] public float? MaxTemp { get; set; }

        [DataMember(Name = "the_temp")] public float? TheTemp { get; set; }

        [DataMember(Name = "wind_speed")] public float? WindSpeed { get; set; }

        [DataMember(Name = "wind_direction")] public float? WindDirection { get; set; }

        [DataMember(Name = "humidity")] public byte? Humidity { get; set; }

        [DataMember(Name = "visibility")] public float? Visibility { get; set; }

        [DataMember(Name = "air_pressure")] public float? AirPressure { get; set; }

        [DataMember(Name = "predictability")] public byte? Predictability { get; set; }

        public Season Season
        {
            get
            {
                if (!DateTime.TryParse(ApplicableDate, out var date)) return Season.Unknown;
                switch (date.Month)
                {
                    case 12:
                    case 1:
                    case 2:
                        return Season.Winter;
                    case 3:
                    case 4:
                    case 5:
                        return Season.Spring;
                    case 6:
                    case 7:
                    case 8:
                        return Season.Summer;
                    case 9:
                    case 10:
                    case 11:
                        return Season.Autumn;
                }

                return Season.Unknown;
            }
        }
    }
}