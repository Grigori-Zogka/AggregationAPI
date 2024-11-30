using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Interfaces;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("headlines")]
        public async Task<IActionResult> GetTopHeadlines(string category = "general")
        {
            var newsResponse = await _newsService.GetTopHeadlinesAsync(category);
            if (newsResponse == null)
            {
                return NotFound("News data not found.");
            }
            return Ok(newsResponse); // Returns the full NewsApiResponse object
        }
    }
}
