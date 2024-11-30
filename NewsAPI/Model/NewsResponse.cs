namespace NewsAPI.Model
{
    public class NewsResponse
    {
        public SourceResponse Source { get; set; } // Details about the source of the news
        public string Author { get; set; }         // Author of the article (can be null)
        public string Title { get; set; }          // Title of the article
        public string Description { get; set; }    // Short description of the article
        public string Url { get; set; }            // URL to the full article
        public string UrlToImage { get; set; }     // URL to an image related to the article
        public DateTime PublishedAt { get; set; }  // Publication date and time
        public string Content { get; set; }
    }

    public class SourceResponse
    {
        public string Id { get; set; }    // ID of the source (can be null)
        public string Name { get; set; }  // Name of the source (e.g., "BBC News")
    }
}
