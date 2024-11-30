using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace APIAggregation.Data
{
    public class AggregationData
    {
        //[BsonId]
        //public Guid Id { get; set; }
        public string ApiType { get; set; }

        [BsonElement("RawData")]
        public BsonDocument RawData { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
