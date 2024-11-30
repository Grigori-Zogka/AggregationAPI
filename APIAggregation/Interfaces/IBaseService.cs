namespace APIAggregation.Interfaces
{
    public interface IBaseService
    {
        Task<object> GetDataAsync(string city, string spotifyQuery, string newsCategory);
    }
}
