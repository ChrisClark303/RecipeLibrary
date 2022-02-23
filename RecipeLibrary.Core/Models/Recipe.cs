using System;
using System.Linq;

namespace RecipeLibrary.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public int Serves { get; set; }
        public RecipeIngredient[] Ingredients { get; set; } = new RecipeIngredient[0];

        public decimal Calories
        {
            get
            {
                return Ingredients.Sum(i => i.Calories);
            }
        }

        public int CaloriesPerPerson
        {
            get
            {
                return (int)Math.Ceiling(Calories / Serves);
            }
        }
    }

    public class Meal
    {
        public string Name { get; set; }
    }
}
