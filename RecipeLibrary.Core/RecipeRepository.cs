using AutoMapper;
using Newtonsoft.Json;
using RecipeLibrary.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public RecipeRepository(IFileSystem fileSystem, IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _fileSystem = fileSystem;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<Recipe[]> Load()
        {
            var content = await _fileSystem.LoadJson("recipes.json");
            var recipeDocs = await _fileSystem.LoadDocument<RecipeDocument>("recipes.json");
            var ingredients = await _ingredientRepository.Load();
            var recipes = _mapper.Map<Recipe[]>(recipeDocs, opts => opts.Items["ingredients"] = ingredients);
            // return JsonConvert.DeserializeObject<Recipe[]>(content, new RecipeIngredientJsonConverter(ingredients));
            return recipes;
        }

        public async Task<Recipe> LoadById(string recipeId)
        {
            var recipe = await _fileSystem.LoadDocument<RecipeDocument>("recipes.json", recipeId);
            var ingredients = await _ingredientRepository.Load();
            var target = _mapper.Map<Recipe>(recipe, opts => opts.Items["ingredients"] = ingredients);
            return target;
        }

        public async Task Save(Recipe[] recipes)
        {
            var ingredients = await _ingredientRepository.Load();
            var json = JsonConvert.SerializeObject(recipes, new RecipeIngredientJsonConverter(ingredients));
            await _fileSystem.Save("recipes.json", json);
        }
    }
}
