using MongoDB.Driver;
using RecipeLibrary.Core.Models;
using RecipeLibrary.Data;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace RecipeLibrary.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoConnection _connection;
        private readonly IMongoQueryBuilder _queryBuilder;

        public RecipeService(IMongoConnection connection, IMongoQueryBuilder queryBuilder)
        {
            _connection = connection;
            _queryBuilder = queryBuilder;
        }

        public async Task AddRecipe(RecipeLight recipe)
        {
            var collection = _connection.Connect<RecipeLight>("recipes");
            await collection.InsertOneAsync(recipe);
        }

        public async Task<RecipeLight[]> GetRecipes()
        {
            var filter = Builders<RecipeLight>.Filter.Empty;
            var projection = Builders<RecipeLight>.Projection.Exclude("_id");
            var collection = _connection.Connect<RecipeLight>("recipes");
            var results = (await collection.Find(filter)
                .Project<RecipeLight>(projection)
                .ToListAsync())
                .ToArray();

            return results;
        }

        public async Task<RecipeLight> GetRandomRecipe()
        {
            //var filter = Builders<RecipeLight>.Filter.Empty;
            //var projection = Builders<RecipeLight>.Projection.Exclude("_id");
            //var collection = _connection.Connect<RecipeLight>("recipes");
            //var results = (await collection.Find(filter)
            //    .Project<RecipeLight>(projection)
            //    .ToListAsync())
            //    .ToArray
            //
            //

            var sampleAggregation = _queryBuilder.WithAggregationSample(1)
                .WithAggregationProjection(new[] { "_id" }, ProjectionType.Exclude)
                .BuildPipeline<RecipeLight,RecipeLight>();
            var collection = _connection.Connect<RecipeLight>("recipes");
            var result = await collection.AggregateAsync(sampleAggregation);
                
            return await result.FirstAsync();
        }

    }
}
