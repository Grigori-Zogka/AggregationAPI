using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Interfaces;

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
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("Invalid parameters.");
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(city);
            
                if (weatherData == null)
                {
                    return NotFound();
                }
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
