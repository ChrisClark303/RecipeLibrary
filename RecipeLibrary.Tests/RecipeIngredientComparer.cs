using RecipeLibrary.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RecipeLibrary.Tests
{
    public class RecipeIngredientComparer : EqualityComparer<RecipeIngredient>
    {
        public override bool Equals([AllowNull] RecipeIngredient x, [AllowNull] RecipeIngredient y)
        {
            return x.Quantity == y.Quantity &&
                x.Calories == y.Calories &&
                new IngredientComparer().Equals(x.Ingredient, y.Ingredient);
        }

        public override int GetHashCode([DisallowNull] RecipeIngredient obj)
        {
            return obj.Ingredient.Name.GetHashCode();
        }
    }
}
