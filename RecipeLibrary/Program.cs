using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace RecipeLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = $"mongodb+srv://movie-user:movies1@cluster0.vqjkx.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            var client = new MongoClient(connString);
            var database = client.GetDatabase("sample_mflix");

            var coll = database.GetCollection<BsonDocument>("movies");
            var result = coll.Find(new BsonDocument())
               .SortByDescending(m => m["runtime"])
               .Limit(10)
               .ToList();
            foreach (var r in result)
            {
                Console.WriteLine(r.GetValue("title"));
            }

            Console.ReadLine();
        }
    }
}
