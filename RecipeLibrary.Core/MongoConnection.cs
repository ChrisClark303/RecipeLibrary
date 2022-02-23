using MongoDB.Bson;
using MongoDB.Driver;

namespace RecipeLibrary.Core
{
    public class MongoConnection : IMongoConnection
    {
        private readonly IMongoSettings _settings;

        public MongoConnection(IMongoSettings settings)
        {
            _settings = settings;
        }

        public IMongoCollection<BsonDocument> Connect(string collectionName)
        {
            var connString = $"mongodb+srv://{_settings.UserName}:{_settings.Password}@{_settings.Uri}?retryWrites=true&w=majority";
            var client = new MongoClient(connString);
            var database = client.GetDatabase("RecipeData");

            return database.GetCollection<BsonDocument>(collectionName);
        }
    }
}
