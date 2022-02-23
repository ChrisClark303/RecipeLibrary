using Moq;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RecipeLibrary.Tests
{
    public class IngredientLibraryTests
    {
        private IngredientLibrary CreateLibrary(Mock<IIngredientRepository> ingredientRepository = null)
        {
            return new IngredientLibrary((ingredientRepository ?? new Mock<IIngredientRepository>()).Object);
        }

        [Fact]
        public async Task Load_CallsIngredientRepository_Load()
        {
            var ingredientRepository = new Mock<IIngredientRepository>();
            var library = CreateLibrary(ingredientRepository);

            await library.Load();

            ingredientRepository.Verify(repo => repo.Load());
        }

        [Fact]
        public async Task Load_SetsIngredients_WithIngredientsReturnedFromRepository()
        {
            var ingredients = new[]
            {
                new StandardIngredient(),
                new StandardIngredient()
            };
            var ingredientRepository = new Mock<IIngredientRepository>();
            ingredientRepository.Setup(repo => repo.Load())
                .ReturnsAsync(ingredients);
            
            var library = CreateLibrary(ingredientRepository);

            await library.Load();

            Assert.Collection(library.Ingredients, ing => Assert.Equal(ingredients[0], ing)
            , ing => Assert.Equal(ingredients[1], ing));
        }

        [Fact]
        public void AddIngredient_AddsIngredientToIngredientsList()
        {
            var ingredient1 = new StandardIngredient();
            var ingredient2 = new StandardIngredient();

            var library = CreateLibrary();

            library.AddIngredient(ingredient1);
            library.AddIngredient(ingredient2);

            Assert.Collection(library.Ingredients, ing => Assert.Equal(ingredient1, ing)
            , ing => Assert.Equal(ingredient2, ing));
        }

        [Fact]
        public async Task Save_CallsIngredientRepository_Save()
        {
            var ingredientRepository = new Mock<IIngredientRepository>();
            var library = CreateLibrary(ingredientRepository);

            await library.Save();

            ingredientRepository.Verify(repo => repo.Save(It.IsAny<StandardIngredient[]>()));
        }

        [Fact]
        public async Task Save_CallsIngredientRepository_Save_WithIngredients()
        {
            var ingredient1 = new StandardIngredient() { Name = "Onion" };
            var ingredient2 = new StandardIngredient() { Name = "Tomato" };
            var ingredient3 = new StandardIngredient() { Name = "Cucumber" };

            var ingredientRepository = new Mock<IIngredientRepository>();

            var library = CreateLibrary(ingredientRepository);
            library.AddIngredient(ingredient1);
            library.AddIngredient(ingredient2);
            library.AddIngredient(ingredient3);

            await library.Save();

            ingredientRepository.Verify(repo => repo.Save(It.Is<StandardIngredient[]>(ings => IngredientsListCorrect(ings, ingredient1, ingredient2, ingredient3))));
        }

        private bool IngredientsListCorrect(StandardIngredient[] actual, params StandardIngredient[] expected)
        {
            return expected.Intersect(actual, new IngredientComparer()).Count() == expected.Count();
        }

        [Fact]
        public async Task Search_ReturnsIngredients_MatchingTerm()
        {
            var ingredient1 = new StandardIngredient() { Name = "Cherry" };
            var ingredient2 = new StandardIngredient() { Name = "Cucumber" };
            var ingredient3 = new StandardIngredient() { Name = "Cherry Tomato" };

            var ingredientRepository = new Mock<IIngredientRepository>();
            ingredientRepository.Setup(repo => repo.Load())
                .ReturnsAsync(new[] { ingredient1, ingredient2, ingredient3 });

            var library = CreateLibrary(ingredientRepository);
            await library.Load();

            var matches = await library.Search("Cherry");

            Assert.Collection(matches, ing => Assert.Equal(ingredient1, ing),
                                        ing => Assert.Equal(ingredient3, ing));
        }

        [Fact]
        public async Task Search_ReturnsIngredients_WhereAnyWord_MatchesTerm()
        {
            var ingredient1 = new StandardIngredient() { Name = "Cucumber" };
            var ingredient2 = new StandardIngredient() { Name = "Plum Tomato" };
            var ingredient3 = new StandardIngredient() { Name = "Tomato" };

            var ingredientRepository = new Mock<IIngredientRepository>();
            ingredientRepository.Setup(repo => repo.Load())
                .ReturnsAsync(new[] { ingredient1, ingredient2, ingredient3 });

            var library = CreateLibrary(ingredientRepository);
            await library.Load();

            var matches = await library.Search("Tomato");

            Assert.Collection(matches, ing => Assert.Equal(ingredient2, ing),
                                        ing => Assert.Equal(ingredient3, ing));
        }

        [Fact]
        public async Task Search_ReturnsIngredients_WhereAnyWord_MatchesTerm_CaseInsensitive()
        {
            var ingredient1 = new StandardIngredient() { Name = "Cucumber" };
            var ingredient2 = new StandardIngredient() { Name = "Plum Tomato" };
            var ingredient3 = new StandardIngredient() { Name = "Tomato" };

            var ingredientRepository = new Mock<IIngredientRepository>();
            ingredientRepository.Setup(repo => repo.Load())
                .ReturnsAsync(new[] { ingredient1, ingredient2, ingredient3 });

            var library = CreateLibrary(ingredientRepository);
            await library.Load();

            var matches = await library.Search("tOmato");

            Assert.Collection(matches, ing => Assert.Equal(ingredient2, ing),
                                        ing => Assert.Equal(ingredient3, ing));
        }
    }
}
