using RecipeLibrary.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public interface IRecipeRepository
    {
        Task<Recipe[]> Load();
        Task Save(Recipe[] recipes);
    }
}