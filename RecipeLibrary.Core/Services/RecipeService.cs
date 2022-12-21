using MongoDB.Driver;
using RecipeLibrary.Core.Models;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace RecipeLibrary.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoConnection _connection;

        public RecipeService(IMongoConnection connection)
        {
            _connection = connection;
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

    }
}
