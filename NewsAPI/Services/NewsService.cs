
using NewsAPI.Interfaces;
using NewsAPI.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("NewsAPI");
            _apiKey = configuration["NewsApiSettings:ApiKey"];
            
        }
        public async Task<IEnumerable<NewsResponse>> GetTopHeadlinesAsync(string category)
        {
            var requestUri = $"top-headlines?category={category}&apiKey={_apiKey}";
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed. Status: {response.StatusCode}, Details: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var newsApiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(responseContent);
            return newsApiResponse?.Articles ?? Enumerable.Empty<NewsResponse>();
            
        }
    }
}
