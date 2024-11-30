using Newtonsoft.Json;
using SpotifyAPI.Model;
using System.Net.Http.Headers;

namespace SpotifyAPI.Services
{
    public class SpotifyApiService
    {
        private readonly HttpClient _httpClient;
        private readonly SpotifyAuthService _authService;
        private readonly IConfiguration _config;

        public SpotifyApiService(HttpClient httpClient, SpotifyAuthService authService, IConfiguration config)
        {
            _httpClient = httpClient;
            _authService = authService;
            _config = config;
        }

        public async Task<dynamic> SearchTracksAsync(string query)
        {
            var accessToken = await _authService.GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var apiUrl = _config["Spotify:ApiUrl"];
            //https://developer.spotify.com/documentation/web-api/reference/search
            var response = await _httpClient.GetAsync($"{apiUrl}search?q={query}&type=track");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var spotifyResponseTracks = JsonConvert.DeserializeObject<dynamic>(responseString);

            var tracks = new List<SpotifyResponse>();

            foreach (var item in spotifyResponseTracks.tracks.items)
            {
                tracks.Add(new SpotifyResponse
                {
                    TrackName = item.name,
                    ArtistName = item.artists[0].name,
                    AlbumName = item.album.name,
                    ReleaseDate = item.album.release_date,
                    SpotifyUrl = item.external_urls.spotify
                });
            }

            return tracks;
        }
    }
}
