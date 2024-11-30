using MongoDB.Bson;
using MongoDB.Driver;
using WeatherAPI.Data;

namespace WeatherAPI.Repository
{
    public class WeatherRepository
    {
        private readonly IMongoCollection<WeatherData> _weatherCollection;
        public WeatherRepository(MongoDBContext context)
        {
            _weatherCollection = context.WeatherDataCollection;
        }

        public async Task AddWeatherDataAsync(string city, string rawJson)
        {
            var weatherData = new WeatherData
            {
                Id = $"{city}_{DateTime.UtcNow:yyyyMMddHHmmss}",
                City = city,
                RawData = BsonDocument.Parse(rawJson),
                Timestamp = DateTime.UtcNow
            };

            await _weatherCollection.InsertOneAsync(weatherData);
        }
    }
}
