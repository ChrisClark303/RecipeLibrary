using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public interface IFileSystem
    {
        Task Save(string fileName, string contents);
        Task<string> Load(string fileName);
    }

    public class MongoDbFileSystem : IFileSystem
    {
        private readonly IMongoConnection _conn;

        public MongoDbFileSystem(IMongoConnection conn)
        {
            _conn = conn;
        }

        public async Task<string> Load(string fileName)
        {
            //var mongoConnection = new MongoConnection();
            var collection = _conn.Connect(fileName);

            var filter = Builders<BsonDocument>.Filter.Empty;
            var resultList = await collection.Find(filter).Project("{_id: 0}") //exclude the _id field since json.net does not like it
                .ToListAsync();

            return resultList.ToJson();
        }

        public async Task Save(string fileName, string contents)
        {
            //var mongoConnection = new MongoConnection();
            var collection = _conn.Connect(fileName);

            var docs = BsonSerializer.Deserialize<IEnumerable<BsonDocument>>(contents);

            await collection.InsertManyAsync(docs);
        }
    }
}
