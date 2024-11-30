using APIAggregation.Interfaces;
using APIAggregation.Repository;
using NewsAPI.Interfaces;
using Newtonsoft.Json;
using SpotifyAPI.Services;
using WeatherAPI.Interfaces;

namespace APIAggregation.Services
{
    public class BaseService : IBaseService
    {
        private readonly INewsService _newsService;
        private readonly IOpenWeatherService _weatherService;
        private readonly SpotifyApiService _spotifyApiService;
        private readonly AggregationRepository _aggregationRepository;
        public BaseService(INewsService newsService, IOpenWeatherService weatherService, SpotifyApiService spotifyApiService, AggregationRepository aggregationRepository)
        {
            _newsService = newsService;
            _weatherService = weatherService;
            _spotifyApiService = spotifyApiService;
            _aggregationRepository = aggregationRepository;
        }

        public async Task<object> GetDataAsync(string city, string query, string category = "general")
        {
            var newsResponse = await _newsService.GetTopHeadlinesAsync(category);
            var weatherData = await _weatherService.GetWeatherAsync(city);
            var spotifyData = await _spotifyApiService.SearchTracksAsync(query);

            if (newsResponse == null || weatherData == null ||spotifyData== null)
            {
                return null;
            }

            try
            {
                var newsResponseJson = JsonConvert.SerializeObject(newsResponse);
                var weatherDataJson = JsonConvert.SerializeObject(weatherData);
                var spotifyDataJson = JsonConvert.SerializeObject(spotifyData);

                await _aggregationRepository.SaveDataAsync("News", newsResponseJson);
                await _aggregationRepository.SaveDataAsync("Weather", weatherDataJson);
                await _aggregationRepository.SaveDataAsync("Spotify", spotifyDataJson);

            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error saving aggregation data: {ex.Message}");
                throw;
            }


            return new
            {
                News = newsResponse,
                Weather = weatherData,
                Track = spotifyData
            };
        }
    }
}
