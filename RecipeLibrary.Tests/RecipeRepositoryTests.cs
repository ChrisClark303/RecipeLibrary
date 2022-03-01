using System.Threading.Tasks;
using Xunit;
using Moq;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace RecipeLibrary.Tests
{
    public partial class RecipeRepositoryTests
    {
        private RecipeRepository CreateRepository(Mock<IFileSystem> fs = null, Mock<IIngredientRepository> ingredientRepo = null, Mock<IMapper> mapper = null)
        {
            return new RecipeRepository((fs ?? CreateFileSystem()).Object, (ingredientRepo ?? new Mock<IIngredientRepository>()).Object, (mapper ?? new Mock<IMapper>()).Object);
        }

        private Mock<IIngredientRepository> CreateIngredientRepository(StandardIngredient[] ingredients)
        {
            var repo = new Mock<IIngredientRepository>();
            repo.Setup(r => r.Load())
                .ReturnsAsync(ingredients);

            return repo;
        }

        [Fact]
        public async Task Load_CallsFileSystem_Load()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = CreateRepository(fs);

            await repo.Load();

            fs.Verify(x => x.LoadJson(It.IsAny<string>()));
        }

        [Fact]
        public async Task Load_CallsFileSystem_Load_WithFileName()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = CreateRepository(fs);

            await repo.Load();

            fs.Verify(x => x.LoadJson("recipes.json"));
        }

        private string _recipeJson = "[{'name':'Onion Salad', 'serves' : 2, ingredients:[{'ingredient' : 'Onion', Quantity: 0.5},{'ingredient' : 'Cucumber', Quantity: 0.5}]},{'name': 'Kosambari', 'serves' : 4, ingredients:[{'ingredient' : 'Carrot', Quantity: 1},{'ingredient' : 'White Cabbage', Quantity: 0.25}]},{'name' : 'Chilli', 'serves' : 6, ingredients:[{'ingredient' : 'Onion', Quantity: 1},{'ingredient' : 'Beef', Quantity: 500},{'ingredient' : 'Pepper', Quantity: 1}]}]";

        [Fact]
        public async Task Load_ReturnsRecipes_FromFileSystem()
        {
            Mock<IFileSystem> fs = CreateFileSystem();

            var repo = CreateRepository(fs);

            var recipes = await repo.Load();

            Assert.Collection(recipes, recipe =>
            {
                Assert.Equal("Onion Salad", recipe.Name);
                Assert.Equal(2, recipe.Serves);
            }, recipe =>
            {
                Assert.Equal("Kosambari", recipe.Name);
                Assert.Equal(4, recipe.Serves);
            }, recipe =>
            {
                Assert.Equal("Chilli", recipe.Name);
                Assert.Equal(6, recipe.Serves);
            });
        }

        [Fact]
        public async Task Load_ReturnsRecipes_FromFileSystem_WithIngredientsSet()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            
            StandardIngredient
                onion = new StandardIngredient() { Name = "Onion", MeasurementInfo = new Measurement { Quantity = 1, Measure = "medium" }, Calories = 35 },
                cucumber = new StandardIngredient() { Name = "Cucumber", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                carrot = new StandardIngredient() { Name = "Carrot", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                cabbage = new StandardIngredient() { Name = "White Cabbage", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                beef = new StandardIngredient() { Name = "Beef", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                pepper = new StandardIngredient() { Name = "Pepper", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 };

            StandardIngredient[] ingredients = new[]
            {
                onion,
                cucumber,
                carrot,
                cabbage,
                beef,
                pepper,
            };

            Mock<IIngredientRepository> ingredientRepo = CreateIngredientRepository(ingredients);
            var repo = CreateRepository(fs, ingredientRepo);

            var recipes = await repo.Load();

            Assert.Collection<Recipe>(recipes, recipe =>
            {
                Assert.Equal("Onion Salad", recipe.Name);
                Assert.Equal(2, recipe.Serves);
                Assert.Collection<RecipeIngredient>(recipe.Ingredients, ing =>
                {
                    Assert.Equal(0.5m, ing.Quantity);
                    Assert.Equal(onion, ing.Ingredient);
                }, ing =>
                {
                    Assert.Equal(0.5m, ing.Quantity);
                    Assert.Equal(cucumber, ing.Ingredient);
                });
            }, recipe =>
            {
                Assert.Equal("Kosambari", recipe.Name);
                Assert.Equal(4, recipe.Serves);
                Assert.Collection<RecipeIngredient>(recipe.Ingredients, ing =>
                {
                    Assert.Equal(1, ing.Quantity);
                    Assert.Equal(carrot, ing.Ingredient);
                }, ing =>
                {
                    Assert.Equal(0.25m, ing.Quantity);
                    Assert.Equal(cabbage, ing.Ingredient);
                });
            }, recipe =>
            {
                Assert.Equal("Chilli", recipe.Name);
                Assert.Equal(6, recipe.Serves);
                Assert.Collection<RecipeIngredient>(recipe.Ingredients, ing =>
                {
                    Assert.Equal(1, ing.Quantity);
                    Assert.Equal(onion, ing.Ingredient);
                }, ing =>
                {
                    Assert.Equal(500, ing.Quantity);
                    Assert.Equal(beef, ing.Ingredient);
                }, ing =>
                {
                    Assert.Equal(1, ing.Quantity);
                    Assert.Equal(pepper, ing.Ingredient);
                });
            });
        }

        private Mock<IFileSystem> CreateFileSystem()
        {
            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.LoadJson(It.IsAny<string>()))
                .ReturnsAsync(_recipeJson);
            return fs;
        }

        [Fact]
        public async Task Save_CallsFileSystem_Save()
        {
            var fileSystem = CreateFileSystem();

            var repo = CreateRepository(fileSystem);
            await repo.Save(null);

            fileSystem.Verify(fs => fs.Save(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task Save_CallsFileSystem_Save_WithFilename()
        {
            var fileSystem = CreateFileSystem();

            var repo = CreateRepository(fileSystem);
            await repo.Save(null);

            fileSystem.Verify(fs => fs.Save("recipes.json", It.IsAny<string>()));
        }

        [Fact]
        public async Task Save_CallsFileSystem_Save_WithIngredientsJson()
        {
            var fileSystem = CreateFileSystem();

            StandardIngredient
                onion = new StandardIngredient() { Name = "Onion", MeasurementInfo = new Measurement { Quantity = 1, Measure = "medium" }, Calories = 35 },
                cucumber = new StandardIngredient() { Name = "Cucumber", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                carrot = new StandardIngredient() { Name = "Carrot", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                cabbage = new StandardIngredient() { Name = "White Cabbage", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                beef = new StandardIngredient() { Name = "Beef", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 },
                pepper = new StandardIngredient() { Name = "Pepper", MeasurementInfo = new Measurement { Quantity = 1, Measure = "small" }, Calories = 50 };

            StandardIngredient[] ingredients = new[]
            {
                onion,
                cucumber,
                carrot,
                cabbage,
                beef,
                pepper,
            };

            var recipes = new[]
            {
                new Recipe() {Name = "Chilli", Serves = 2, Ingredients = new []
                {
                    new RecipeIngredient() { Quantity = 2, Ingredient = onion },
                    new RecipeIngredient() { Quantity = 500, Ingredient = beef },
                    new RecipeIngredient() { Quantity = 1, Ingredient = pepper }
                }},
                new Recipe() {Name = "Onion Salad", Serves = 2, Ingredients = new [] {
                    new RecipeIngredient() { Quantity = 0.5m, Ingredient = onion },
                    new RecipeIngredient() { Quantity = 0.5m, Ingredient = cucumber}
                }
            }};

            var ingredientRepo = CreateIngredientRepository(ingredients);

            var repo = CreateRepository(fileSystem);
            await repo.Save(recipes);

            fileSystem.Verify(fs => fs.Save(It.IsAny<string>(), It.Is<string>(s => JsonMatchesObjects(s, recipes, ingredients))));
        }

        private bool JsonMatchesObjects(string json, Recipe[] recipes, StandardIngredient[] ingredients)
        {
            var deserializedRecipes = JsonConvert.DeserializeObject<Recipe[]>(json, new RecipeIngredientJsonConverter(ingredients));

            return recipes.Intersect(deserializedRecipes, new RecipeComparer())
                .Count() == recipes.Count();
        }

        private class IngredientComparer : EqualityComparer<StandardIngredient>
        {
            public override bool Equals([AllowNull] StandardIngredient x, [AllowNull] StandardIngredient y)
            {
                return x.Name == y.Name &&
                    x.Measurement == y.Measurement &&
                    x.Calories == y.Calories;
            }

            public override int GetHashCode([DisallowNull] StandardIngredient obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        [Fact]
        public async Task LoadById_CallsFileSystem_Load()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = CreateRepository(fs);

            await repo.LoadById("");

            fs.Verify(x => x.LoadDocument<RecipeDocument>(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task LoadById_CallsFileSystem_Load_WithFileName()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = CreateRepository(fs);

            await repo.LoadById("");

            fs.Verify(x => x.LoadDocument<RecipeDocument>("recipes.json", It.IsAny<string>()));
        }

        [Fact]
        public async Task LoadById_CallsFileSystem_Load_WithRecipeId()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = CreateRepository(fs);

            await repo.LoadById("12345");

            fs.Verify(x => x.LoadDocument<RecipeDocument>(It.IsAny<string>(), "12345"));
        }
    }
}
