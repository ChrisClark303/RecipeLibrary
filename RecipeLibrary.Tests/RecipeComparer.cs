using RecipeLibrary.Models;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RecipeLibrary.Tests
{
    public class RecipeComparer : EqualityComparer<Recipe>
    {
        public override bool Equals([AllowNull] Recipe x, [AllowNull] Recipe y)
        {
            return x.Name == y.Name &&
                x.Serves == y.Serves &&
                x.Calories == y.Calories &&
                x.Ingredients.Count() == y.Ingredients.Count() &&
                x.Ingredients.Intersect(y.Ingredients, new RecipeIngredientComparer()).Count() == x.Ingredients.Count();
        }

        public override int GetHashCode([DisallowNull] Recipe obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
