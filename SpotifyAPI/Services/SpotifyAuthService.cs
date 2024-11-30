using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace SpotifyAPI.Services
{
    public class SpotifyAuthService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SpotifyAuthService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var clientId = _config["Spotify:ClientId"];
            var clientSecret = _config["Spotify:ClientSecret"];
            var tokenUrl = _config["Spotify:TokenUrl"];

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var response = await _httpClient.PostAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

            return tokenResponse.access_token;
        }
    }
}
