using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WeatherAPI.Data
{
    public class WeatherData
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("City")]
        public string City { get; set; } = null!;

        [BsonElement("RawData")]
        public BsonDocument RawData { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
