using RecipeLibrary.Models;
using Xunit;

namespace RecipeLibrary.Tests
{
    public class RecipeIngredientTests
    {
        [Fact]
        public void Calories_CorrectlyCalculateCalories_BasedOnStandardIngredientCalories()
        {
            var standard = new StandardIngredient()
            {
                Calories = 10
            };
            var recipe = new RecipeIngredient()
            {
                Ingredient = standard,
                Quantity = 1
            };

            Assert.Equal(10, recipe.Calories);
        }

        [Fact]
        public void Calories_CorrectlyCalculateCalories_BasedOnQuantity()
        {
            var standard = new StandardIngredient()
            {
                Calories = 10
            };
            var recipe = new RecipeIngredient()
            {
                Ingredient = standard,
                Quantity = 2.5m
            };

            Assert.Equal(25, recipe.Calories);
        }

        [Fact]
        public void Calories_Zero_WhenStandardIngredientIsNull()
        {
            var recipe = new RecipeIngredient()
            {
                Ingredient = null,
                Quantity = 2.5m
            };

            Assert.Equal(0, recipe.Calories);
        }

        [Fact]
        public void Measurement_Null_WhenStandardIngredientIsNull()
        {
            var recipe = new RecipeIngredient()
            {
                Ingredient = null,
                Quantity = 2.5m
            };

            Assert.Null(recipe.Measurement);
        }

        [Fact]
        public void Measure_CorrectlyCalculatesMeasurement_BasedOnQuantity()
        {
            var standard = new StandardIngredient()
            {
                MeasurementInfo = new Measurement
                {
                    Measure = "small",
                    Quantity = 12
                }
            };
            var recipe = new RecipeIngredient()
            {
                Ingredient = standard,
                Quantity = 2
            };

            Assert.Equal("24 small", recipe.Measurement);
        }

        [Fact]
        public void Measure_CorrectlyCalculatesMeasurement_BasedOnQuantity_WithTrailingZerosRemoved()
        {
            var standard = new StandardIngredient()
            {
                MeasurementInfo = new Measurement
                {
                    Measure = "small",
                    Quantity = 12
                }
            };
            var recipe = new RecipeIngredient()
            {
                Ingredient = standard,
                Quantity = 2.0m
            };

            Assert.Equal("24 small", recipe.Measurement);
        }

        [Fact]
        public void Measure_CorrectlyCalculatesMeasurement_BasedOnQuantity_WithCorrectDecimalPlaces()
        {
            var standard = new StandardIngredient()
            {
                MeasurementInfo = new Measurement
                {
                    Measure = "tbsp",
                    Quantity = 0.5m
                }
            };
            var recipe = new RecipeIngredient()
            {
                Ingredient = standard,
                Quantity = 3
            };

            Assert.Equal("1.5 tbsp", recipe.Measurement);
        }
    }
}
