using RecipeLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public class RecipeLibrary : IRecipeLibrary
    {
        private readonly IRecipeRepository _recipeRepository;

        private List<Recipe> _recipes = new List<Recipe>();
        public Recipe[] Recipes
        {
            get { return _recipes.ToArray(); }
        }

        public RecipeLibrary(IRecipeRepository repository)
        {
            _recipeRepository = repository;
        }

        public async Task Load()
        {
            var recipes = await _recipeRepository.Load();
            _recipes = new List<Recipe>(recipes);
        }

        public async Task Save()
        {
            await _recipeRepository.Save(_recipes.ToArray());
        }

        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public Recipe[] Search(params StandardIngredient[] ingredients)
        {
            //TODO : Provide a way of searching for recipes based on ingredients
            return null;
        }
    }
}
