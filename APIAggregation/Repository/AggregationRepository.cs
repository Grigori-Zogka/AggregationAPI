using APIAggregation.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NewsAPI.Model;

namespace APIAggregation.Repository
{
    public class AggregationRepository
    {
        private readonly IMongoCollection<AggregationData> _aggrigationCollection;
        private readonly ILogger<AggregationRepository> _logger;
        public AggregationRepository(MongoDBContext context, ILogger<AggregationRepository> logger)
        {
            _aggrigationCollection = context.WeatherDataCollection;
            _logger = logger;
        }

        public async Task SaveDataAsync(string type,string rawJson)
        {
            try
            {
                BsonValue bsonData;
                string id = $"Data_{type}-{DateTime.UtcNow}";
                // Determine if the input JSON is an array or an object
                if (rawJson.TrimStart().StartsWith("["))
                {
                    var bsonArray = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(rawJson);

                    bsonData = new BsonDocument { { "ArrayData", bsonArray } };
                }
                else
                {
                    bsonData = BsonDocument.Parse(rawJson);
                }

                var aggregationData = new AggregationData
                {   Id =  id,
                    ApiType = type,
                    RawData = (BsonDocument)bsonData,
                    Timestamp = DateTime.UtcNow
                };

                _logger.LogInformation($"Entry Saved : {id}");
                await _aggrigationCollection.InsertOneAsync(aggregationData);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving aggregation data: {ex.Message}");
                throw;
            }
          
        }
      
    }
}
