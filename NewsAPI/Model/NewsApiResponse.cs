namespace NewsAPI.Model
{
    public class NewsApiResponse
    {
        public string Status { get; set; }         // "ok", "error", etc.
        public int TotalResults { get; set; }     // Total number of articles available
        public IEnumerable<NewsResponse> Articles { get; set; } // List of news articles
    }
}
