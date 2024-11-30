using APIAggregation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly BaseService _baseService;

        public AggregationController(BaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet("{Aggegation}")]
        public async Task<IActionResult> GetNewsAndWeather(string city, string spotifyQuery,string newsCategory = "general")
        {
            var result = await _baseService.GetNewsAndWeatherAsync(city, spotifyQuery, newsCategory);
            if (result == null)
            {
                return NotFound("Unable to fetch aggregation data.");
            }

            return Ok(result);
        }
    }
}
