using APIAggregation.Interfaces;
using APIAggregation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly IBaseService _baseService;

        public AggregationController(IBaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet] 
        public async Task<IActionResult> GetNewsAndWeather(string city, string spotifyQuery,string newsCategory = "general")
        {
            var result = await _baseService.GetDataAsync(city, spotifyQuery, newsCategory);
            if (result == null)
            {
                return NotFound("Unable to fetch aggregation data.");
            }

            return Ok(result);
        }
    }
}
