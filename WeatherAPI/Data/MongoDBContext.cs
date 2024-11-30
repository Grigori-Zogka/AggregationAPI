using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WeatherAPI.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<WeatherData> WeatherDataCollection =>
          _database.GetCollection<WeatherData>("OpenWeatherMap");
    }


    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
