using Newtonsoft.Json;
using RecipeLibrary.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{

    public class RecipeRepository : IRecipeRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeRepository(IFileSystem fileSystem, IIngredientRepository ingredientRepository)
        {
            _fileSystem = fileSystem;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Recipe[]> Load()
        {
            var content = await _fileSystem.Load("recipes.json");
            var ingredients = await _ingredientRepository.Load();
            return JsonConvert.DeserializeObject<Recipe[]>(content, new RecipeIngredientJsonConverter(ingredients));
        }

        public async Task Save(Recipe[] recipes)
        {
            var ingredients = await _ingredientRepository.Load();
            var json = JsonConvert.SerializeObject(recipes, new RecipeIngredientJsonConverter(ingredients));
            await _fileSystem.Save("recipes.json", json);
        }
    }
}
