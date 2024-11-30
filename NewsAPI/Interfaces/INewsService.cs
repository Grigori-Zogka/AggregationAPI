using NewsAPI.Model;

namespace NewsAPI.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsApiResponse>> GetTopHeadlinesAsync(string category);
    }
}
