using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public interface IFileSystem
    {
        Task Save(string fileName, string contents);
        Task<string> LoadJson(string fileName);
        Task<IEnumerable<TDocType>> LoadDocument<TDocType>(string fileName, params string[] exclude);
        Task<TDocType> LoadDocument<TDocType>(string fileName, string docId) where TDocType : DocumentBase;
    }

    public class MongoDbFileSystem : IFileSystem
    {
        private readonly IMongoConnection _conn;

        public MongoDbFileSystem(IMongoConnection conn)
        {
            _conn = conn;
        }

        public async Task<string> LoadJson(string fileName)
        {
            var collection = _conn.Connect(fileName);

            var filter = Builders<BsonDocument>.Filter.Empty;
            var resultList = await collection.Find(filter).Project("{_id: 0}") //exclude the _id field since json.net does not like it
                .ToListAsync();

            return resultList.ToJson();
        }

        public async Task<IEnumerable<TDocType>> LoadDocument<TDocType>(string fileName, params string[] exclude)
        {
            var collection = _conn.Connect<TDocType>(fileName);
            var resultList = collection
                .Find<TDocType>(Builders<TDocType>.Filter.Empty);
                //.Project<TDocType>(Builders<TDocType>.Projection
                //    .Exclude(exclude.FirstOrDefault()))
                //.ToListAsync();

            if (exclude.Any())
            {
                var project = Builders<TDocType>.Projection;
                ProjectionDefinition<TDocType> projDef = null;
                foreach (string s in exclude)
                {
                    projDef = project.Exclude(s);
                }

                resultList = resultList.Project<TDocType>(projDef);
            }
            return await resultList.ToListAsync();
        }

        public async Task<TDocType> LoadDocument<TDocType>(string fileName, string docId) where TDocType : DocumentBase
        {
            var collection = _conn.Connect<TDocType>(fileName);
            var resultList = collection
                .Find<TDocType>(Builders<TDocType>.Filter.Eq(d => d.Id, docId));

            return await resultList.FirstOrDefaultAsync();
        }

        public async Task Save(string fileName, string contents)
        {
            var collection = _conn.Connect(fileName);

            var docs = BsonSerializer.Deserialize<IEnumerable<BsonDocument>>(contents);

            await collection.InsertManyAsync(docs);
        }
    }
}
