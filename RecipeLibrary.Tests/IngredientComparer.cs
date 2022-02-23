using RecipeLibrary.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RecipeLibrary.Tests
{
    public class IngredientComparer : EqualityComparer<StandardIngredient>
    {
        public override bool Equals([AllowNull] StandardIngredient x, [AllowNull] StandardIngredient y)
        {
            return x.Name == y.Name &&
                x.MeasurementInfo?.Measure == y.MeasurementInfo?.Measure &&
                x.MeasurementInfo?.Quantity == y.MeasurementInfo?.Quantity &&
                x.Calories == y.Calories;
        }

        public override int GetHashCode([DisallowNull] StandardIngredient obj)
        {
            return (obj.Name ?? "").GetHashCode();
        }
    }
}
