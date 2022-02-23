using RecipeLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLibrary.Core
{
    public class IngredientLibrary : IIngredientLibrary
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientLibrary(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        private List<StandardIngredient> _ingredients = new List<StandardIngredient>();
        public StandardIngredient[] Ingredients
        {
            get { return _ingredients.ToArray(); }
        }

        public async Task Load()
        {
            var ingredients = await _ingredientRepository.Load();
            _ingredients = new List<StandardIngredient>(ingredients);
        }

        public async Task Save()
        {
            await _ingredientRepository.Save(_ingredients.ToArray());
        }

        public void AddIngredient(StandardIngredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public async Task<StandardIngredient[]> Search(string term)
        {
            var matches = _ingredients.Where(ing => ing.Name.Split(' ').Any(word => word.StartsWith(term, System.StringComparison.InvariantCultureIgnoreCase)));
            return matches.ToArray();
        }

        public async Task MigrateMeasurements()
        {
            var measurementMappings = new Dictionary<string, Measurement>
            {
                {"1 medium", new Measurement { Quantity = 1, Measure = "medium" } },
                {"100g", new Measurement { Quantity = 100, Measure = "grams" } },
                {"1tbsp", new Measurement { Quantity = 1, Measure = "tbsp" } },
            };

            await Load();
            foreach (var ingredient in _ingredients)
            {
                ingredient.MeasurementInfo = measurementMappings[ingredient.Measurement];
            }

            await Save();
        }
    }
}
