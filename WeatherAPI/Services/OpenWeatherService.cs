using Newtonsoft.Json;
using WeatherAPI.Interfaces;
using WeatherAPI.Model;


namespace WeatherAPI.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public OpenWeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("OpenWeatherMap");
            _apiKey = configuration["WeatherApiSettings:ApiKey"];

        }

        public async Task<object> GetWeatherAsync(string city)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<WeatherResponse>(content);

            }
            catch (Exception)
            {

                throw;
            }
        }

      
    }
}
