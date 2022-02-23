using RecipeLibrary.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public interface IIngredientRepository
    {
        Task<StandardIngredient[]> Load();
        Task Save(StandardIngredient[] ingredients);
    }
}