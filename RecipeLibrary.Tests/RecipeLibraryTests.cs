using Moq;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RecipeLibrary.Tests
{

    public class RecipeLibraryTests
    {
        private Core.RecipeLibrary CreateLibrary(Mock<IRecipeRepository> recipeRepository = null)
        {
            return new Core.RecipeLibrary((recipeRepository ?? new Mock<IRecipeRepository>()).Object);
        }

        [Fact]
        public async Task Load_CallsRecipeRepository_Load()
        {
            var recipeRepository = new Mock<IRecipeRepository>();
            var library = CreateLibrary(recipeRepository);

            await library.Load();

            recipeRepository.Verify(repo => repo.Load());
        }

        [Fact]
        public async Task Load_SetsRecipes_WithRecipesReturnedFromRepository()
        {
            var recipes = new[]
            {
                new Recipe(),
                new Recipe()
            };
            var recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(repo => repo.Load())
                .ReturnsAsync(recipes);

            var library = CreateLibrary(recipeRepository);

            await library.Load();

            Assert.Collection(library.Recipes, ing => Assert.Equal(recipes[0], ing), 
                ing => Assert.Equal(recipes[1], ing));
        }

        [Fact]
        public void AddRecipe_AddsRecipesToRecipesList()
        {
            var recipe1 = new Recipe();
            var recipe2 = new Recipe();

            var library = CreateLibrary();

            library.AddRecipe(recipe1);
            library.AddRecipe(recipe2);

            Assert.Collection(library.Recipes, recipe => Assert.Equal(recipe1, recipe), 
                recipe => Assert.Equal(recipe2, recipe));
        }

        [Fact]
        public async Task Save_CallsRecipeRepository_Save()
        {
            var recipeRepository = new Mock<IRecipeRepository>();
            var library = CreateLibrary(recipeRepository);

            await library.Save();

            recipeRepository.Verify(repo => repo.Save(It.IsAny<Recipe[]>()));
        }

        [Fact]
        public async Task Save_CallsIngredientRepository_Save_WithRecipes()
        {
            var recipe1 = new Recipe() { Name = "Duck Soup" };
            var recipe2 = new Recipe() { Name = "Hamburger Hill" };
            var recipe3 = new Recipe() { Name = "Soylent Green" };

            var recipeRepository = new Mock<IRecipeRepository>();

            var library = CreateLibrary(recipeRepository);
            library.AddRecipe(recipe1);
            library.AddRecipe(recipe2);
            library.AddRecipe(recipe3);

            await library.Save();

            recipeRepository.Verify(repo => repo.Save(It.Is<Recipe[]>(recipes => RecipesListCorrect(recipes, recipe1, recipe2, recipe3))));
        }

        private bool RecipesListCorrect(Recipe[] actual, params Recipe[] expected)
        {
            return expected.Intersect(actual, new RecipeComparer()).Count() == expected.Count();
        }
    }
}
