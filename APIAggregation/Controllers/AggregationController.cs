using APIAggregation.Interfaces;
using APIAggregation.Repository;
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
        private readonly ILogger<AggregationRepository> _logger;

        public AggregationController(IBaseService baseService, ILogger<AggregationRepository> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        [HttpGet] 
        public async Task<IActionResult> GetNewsAndWeather(string city, string spotifyQuery,string newsCategory = "general")
        {
            _logger.LogInformation($"New Entry. City: {city} , Spotify Query: {spotifyQuery}, NewsCategory: {newsCategory}  Time: {DateTime.UtcNow}");


            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(spotifyQuery) || string.IsNullOrEmpty(newsCategory))
            {
                _logger.LogError($"Invalid parameters. City: {city} , Spotify Query: {spotifyQuery}, NewsCategory: {newsCategory}");
                return BadRequest("Invalid parameters.");
            }

            try
            {
                var result = await _baseService.GetDataAsync(city, spotifyQuery, newsCategory);
                if (result == null)
                {
                    _logger.LogError($"there are no result data");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error:{ ex.Message}");
                return StatusCode(500, "Internal server error:{ ex.Message}");
               
            }

        }
    }
}
