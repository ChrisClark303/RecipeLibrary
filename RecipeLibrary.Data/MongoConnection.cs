using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace RecipeLibrary.Core
{
    public class MongoConnection : IMongoConnection
    {
        private readonly IMongoSettings _settings;

        public MongoConnection(IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;
        }

        public IMongoCollection<BsonDocument> Connect(string collectionName)
        {
            IMongoDatabase database = OpenDatabase();
            return database.GetCollection<BsonDocument>(collectionName);
        }

        private IMongoDatabase OpenDatabase()
        {
            var connString = $"mongodb+srv://{_settings.UserName}:{_settings.Password}@{_settings.Uri}?retryWrites=true&w=majority";
            var client = new MongoClient(connString);
            return client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<TDocType> Connect<TDocType>(string collectionName)
        {
            IMongoDatabase database = OpenDatabase();
            return database.GetCollection<TDocType>(collectionName);
        }
    }
}
