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
            IMongoDatabase database = OpenDatabase();
            return database.GetCollection<BsonDocument>(collectionName);
        }

        private IMongoDatabase OpenDatabase()
        {
            var connString = $"mongodb+srv://{_settings.UserName}:{_settings.Password}@{_settings.Uri}?retryWrites=true&w=majority";
            var client = new MongoClient(connString);
            return client.GetDatabase("RecipeData");
        }

        public IMongoCollection<TDocType> Connect<TDocType>(string collectionName)
        {
            IMongoDatabase database = OpenDatabase();
            return database.GetCollection<TDocType>(collectionName);
        }
    }
}
