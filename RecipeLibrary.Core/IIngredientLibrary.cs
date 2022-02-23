using RecipeLibrary.Models;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public interface IIngredientLibrary
    {
        StandardIngredient[] Ingredients { get; }

        void AddIngredient(StandardIngredient ingredient);
        Task Load();
        Task Save();
        Task<StandardIngredient[]> Search(string term);
        Task MigrateMeasurements();
    }
}