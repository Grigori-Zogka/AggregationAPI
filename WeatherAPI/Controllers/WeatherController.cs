using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Data;
using WeatherAPI.Interfaces;
using WeatherAPI.Repository;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _weatherService;
        
        public WeatherController(IOpenWeatherService weatherService)
        {
            _weatherService = weatherService;
            
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            
            var weatherData = await _weatherService.GetWeatherAsync(city);
            
            if (weatherData == null)
            {
                return NotFound("Weather data not found.");
            }
            return Ok(weatherData);
        }

    }
}
