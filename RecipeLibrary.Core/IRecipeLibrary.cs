using RecipeLibrary.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public interface IRecipeLibrary
    {
        Recipe[] Recipes { get; }

        void AddRecipe(Recipe recipe);
        Task Load();
        Task Save();
        Recipe[] Search(params StandardIngredient[] ingredients);
        Task<Recipe> LoadById(string recipeId);
    }
}