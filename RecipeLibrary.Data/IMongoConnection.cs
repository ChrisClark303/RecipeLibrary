using MongoDB.Bson;
using MongoDB.Driver;

namespace RecipeLibrary.Core
{
    public interface IMongoConnection
    {
        IMongoCollection<BsonDocument> Connect(string collectionName);
        IMongoCollection<TDocType> Connect<TDocType>(string collectionName);
    }
}