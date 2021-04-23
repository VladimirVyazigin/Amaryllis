using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MetaweatherComTests.Entities;
using MetaweatherComTests.Helpers;
using NUnit.Framework;

namespace MetaweatherComTests.Tests
{
    public class MetaweatherComTests
    {
        private const string BaseUrl = "https://www.metaweather.com/api/";

        private readonly HttpClient _client;

        public MetaweatherComTests()
        {
            _client = new HttpClient();
        }

        private async Task<T> GetData<T>(string url)
        {
            using var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonHelper.Deserialize<T>(content);
        }

        private async Task<RealGeoData> GetRealGeoData(Location location)
        {
            var url = "https://api.bigdatacloud.net/data/reverse-geocode-client?" +
                      $"latitude={location.Latitude}&longitude={location.Longitude}&localityLanguage=en";
            return await GetData<RealGeoData>(url);
        }

        private async Task<WeatherInfo> GetWeatherForecastOnToday(int woeid)
        {
            var forecast = await GetData<LocationForecast>(BaseUrl + $"location/{woeid}/");
            return forecast.ConsolidatedWeather.FirstOrDefault(x =>
                x.ApplicableDate != null && DateTime.Parse(x.ApplicableDate).Date == DateTime.Today);
        }

        [TestCase("min", "Minsk")]
        public async Task CheckLocationSearch(string searchPattern, string searchResult)
        {
            var locations = await GetData<Location[]>(BaseUrl + $"location/search/?query={searchPattern}");
            var minsk = locations.FirstOrDefault(x =>
                x.Title.Equals(searchResult, StringComparison.InvariantCultureIgnoreCase));
            Assert.NotNull(minsk);
        }

        [TestCase("min")]
        public async Task CheckLocationCoordinates(string searchPattern)
        {
            var locations = await GetData<Location[]>(BaseUrl + $"location/search/?query={searchPattern}");
            foreach (var location in locations)
            {
                var realGeoData = await GetRealGeoData(location);
                if (realGeoData.City == null)
                {
                    TestContext.WriteLine(
                        $"Unable to check coordinates for '{location.Title}' ({location.LatitudeAndLongitude})");
                    continue;
                }

                if (!realGeoData.City.Equals(location.Title, StringComparison.InvariantCultureIgnoreCase)) Assert.Fail();
            }

            Assert.Pass();
        }

        [TestCase(834463)] //Minsk
        public async Task CheckWeatherForecastOnToday(int woeid)
        {
            var weatherInfo = await GetWeatherForecastOnToday(woeid);
            Assert.NotNull(weatherInfo);
        }

        [TestCase(834463)] //Minsk
        public async Task CheckWeatherForecastOnFiveYearsAgo(int woeid)
        {
            var url = BaseUrl + $"location/{woeid}/" +
                      $"{DateTime.Today.Year - 5}/{DateTime.Today.Month}/{DateTime.Today.Day}";
            var historyForecast = await GetData<WeatherInfo[]>(url);
            var todayForecast = await GetWeatherForecastOnToday(woeid);
            Assert.True(todayForecast != null &&
                        historyForecast.Any(x => x.WeatherStateName == todayForecast.WeatherStateName));
        }

        [TestCase(834463)] //Minsk
        public async Task CheckWindTemperature(int woeid)
        {
            var forecast = await GetData<LocationForecast>(BaseUrl + $"location/{woeid}/");
            if (forecast.ConsolidatedWeather.Any(x => x.TheTemp == null)) Assert.Fail();
            Assert.True(forecast.ConsolidatedWeather.All(x =>
                x.TheTemp < 0 && x.Season == Season.Winter ||
                x.TheTemp > 0 && x.Season == Season.Summer ||
                x.TheTemp > -50 && x.TheTemp < 50 && (x.Season == Season.Spring || x.Season == Season.Autumn)));
        }
    }
}