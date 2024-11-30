
using NewsAPI.Interfaces;
using NewsAPI.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IEnumerable<NewsApiResponse>> GetTopHeadlinesAsync(string category)
        {
            var requestUri = $"top-headlines?category={category}&apiKey={_apiKey}";
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed. Status: {response.StatusCode}, Details: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var newsApiResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

            // Mapping API response to a simplified list of NewsApiResponse
            var articles = new List<NewsApiResponse>();
            if (newsApiResponse?.articles != null)
            {
                foreach (var article in newsApiResponse.articles)
                {
                    var newsArticle = new NewsApiResponse
                    {
                        SourceId = article.source?.id,
                        SourceName = article.source?.name,
                        Author = article.author,
                        Title = article.title,
                        Description = article.description,
                        Url = article.url,
                        UrlToImage = article.urlToImage,
                        PublishedAt = article.publishedAt,
                        Content = article.content
                    };
                    articles.Add(newsArticle);
                }
            }

            return articles;
        }
    }

}


