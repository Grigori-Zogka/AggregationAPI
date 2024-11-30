namespace WeatherAPI.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<object> GetWeatherAsync(string city);
    }
}
