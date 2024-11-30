using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Services;
using System.Diagnostics;

namespace SpotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyApiController : ControllerBase
    {
        private readonly SpotifyApiService _spotifyService;

        public SpotifyApiController(SpotifyApiService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTracks(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Invalid parameters.");
            }

            try
            {
                var result = await _spotifyService.SearchTracksAsync(query);

                if (result == null || result.Count == 0)
                {
                    return NotFound();

                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
