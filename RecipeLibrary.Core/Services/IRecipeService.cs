using RecipeLibrary.Core.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core.Services
{
    public interface IRecipeService
    {
        Task AddRecipe(RecipeLight recipe);
        Task<RecipeLight[]> GetRecipes();
    }
}