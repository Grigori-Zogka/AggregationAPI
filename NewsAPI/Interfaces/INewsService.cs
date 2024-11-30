using NewsAPI.Model;

namespace NewsAPI.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsResponse>> GetTopHeadlinesAsync(string category);
    }
}
