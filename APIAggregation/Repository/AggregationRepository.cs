using APIAggregation.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NewsAPI.Model;

namespace APIAggregation.Repository
{
    public class AggregationRepository
    {
        private readonly IMongoCollection<AggregationData> _aggrigationCollection;
        public AggregationRepository(MongoDBContext context)
        {
            _aggrigationCollection = context.WeatherDataCollection;
        }

        public async Task SaveDataAsync(string type,string rawJson)
        {
            try
            {
                BsonValue bsonData;

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
                {
                    ApiType = type,
                    RawData = (BsonDocument)bsonData,
                    Timestamp = DateTime.UtcNow
                };

                await _aggrigationCollection.InsertOneAsync(aggregationData);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
                throw;
            }
          
        }
      
    }
}
