using RecipeLibrary.Models;
using Xunit;

namespace RecipeLibrary.Tests
{

    public class RecipeTests
    {
        [Fact]
        public void Calories_ReturnsSumOfCalories_InAllIngredients()
        {
            var ingredient1 = new RecipeIngredient
            {
                Ingredient = new StandardIngredient() { Calories = 10 },
                Quantity = 1
            }; // calories = 10
            var ingredient2 = new RecipeIngredient
            {
                Ingredient = new StandardIngredient() { Calories = 15 },
                Quantity = 2
            }; // calories = 30
            var ingredient3 = new RecipeIngredient
            {
                Ingredient = new StandardIngredient() { Calories = 50 },
                Quantity = 0.5m
            }; //calories = 25

            var recipe = new Recipe()
            {
                Ingredients = new[]
                {
                    ingredient1,
                    ingredient2,
                    ingredient3
                }
            };

            Assert.Equal(65, recipe.Calories);
        }

        [Fact]
        public void CaloriesPerPerson_ReturnsSumOfCalories_InAllIngredients_DividedByServes()
        {
            var ingredient1 = new RecipeIngredient
            {
                Ingredient = new StandardIngredient() { Calories = 10 },
                Quantity = 1
            }; // calories = 10
            var ingredient2 = new RecipeIngredient
            {
                Ingredient = new StandardIngredient() { Calories = 15 },
                Quantity = 2
            }; // calories = 30
            var ingredient3 = new RecipeIngredient
            {
                Ingredient = new StandardIngredient() { Calories = 50 },
                Quantity = 0.5m
            }; //calories = 25

            var recipe = new Recipe()
            {
                Ingredients = new[]
                {
                    ingredient1,
                    ingredient2,
                    ingredient3
                },
                Serves = 2
            };

            Assert.Equal(33m, recipe.CaloriesPerPerson);
        }
    }
}
