using System.Threading.Tasks;
using Xunit;
using Moq;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RecipeLibrary.Tests
{
    public class IngredientRepositoryTests
    {
        [Fact]
        public async Task Load_CallsFileSystem_Load()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = new IngredientRepository(fs.Object);

            await repo.Load();

            fs.Verify(x => x.Load(It.IsAny<string>()));
        }

        [Fact]
        public async Task Load_CallsFileSystem_Load_WithFileName()
        {
            Mock<IFileSystem> fs = CreateFileSystem();
            var repo = new IngredientRepository(fs.Object);

            await repo.Load();

            fs.Verify(x => x.Load("ingredients"));
        }

        private string _ingredientJson = "[{'name':'Onion', 'measurementInfo' : { 'quantity' : 1, 'measure': 'small'}, 'calories' : 35},{'name': 'Carrot', 'measurementInfo' : {'quantity' : 2, 'measure': 'medium'}, 'calories' : 45},{'name' : 'Tomato', 'measurementInfo' : {'quantity' : 7, 'measure' : 'large'}, 'calories' : 25}]";

        [Fact]
        public async Task Load_ReturnsStandardIngredients_FromFileSystem()
        {
            Mock<IFileSystem> fs = CreateFileSystem();

            var repo = new IngredientRepository(fs.Object);

            var ingredients = await repo.Load();

            Assert.Collection(ingredients, ing =>
            {
                Assert.Equal("Onion", ing.Name);
                Assert.Equal("1 small", ing.Measurement);
                Assert.Equal(35, ing.Calories);
            }, ing =>
            {
                Assert.Equal("Carrot", ing.Name);
                Assert.Equal("2 medium", ing.Measurement);
                Assert.Equal(45, ing.Calories);
            }, ing =>
            {
                Assert.Equal("Tomato", ing.Name);
                Assert.Equal("7 large", ing.Measurement);
                Assert.Equal(25, ing.Calories);
            });
        }

        private Mock<IFileSystem> CreateFileSystem()
        {
            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.Load(It.IsAny<string>()))
                .ReturnsAsync(_ingredientJson);
            return fs;
        }

        [Fact]
        public async Task Save_CallsFileSystem_Save()
        {
            var fileSystem = CreateFileSystem();

            var repo = new IngredientRepository(fileSystem.Object);
            await repo.Save(null);

            fileSystem.Verify(fs => fs.Save(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task Save_CallsFileSystem_Save_WithFilename()
        {
            var fileSystem = CreateFileSystem();

            var repo = new IngredientRepository(fileSystem.Object);
            await repo.Save(null);

            fileSystem.Verify(fs => fs.Save("ingredients", It.IsAny<string>()));
        }

        [Fact]
        public async Task Save_CallsFileSystem_Save_WithIngredientsJson()
        {
            var fileSystem = CreateFileSystem();

            var repo = new IngredientRepository(fileSystem.Object);
            StandardIngredient[] ingredients = new[]
            {
                new StandardIngredient() { Name = "Onion", MeasurementInfo = new Measurement {Quantity = 1, Measure = "medium" }, Calories = 35},
                new StandardIngredient() { Name = "Turnip", MeasurementInfo = new Measurement {Quantity = 1, Measure = "small" }, Calories = 50}
            };
            await repo.Save(ingredients);

            fileSystem.Verify(fs => fs.Save(It.IsAny<string>(), It.Is<string>(s => JsonMatchesObjects(s, ingredients))));
        }

        private bool JsonMatchesObjects(string json, StandardIngredient[] ingredients)
        {
            var deserializedIngredients = JsonConvert.DeserializeObject<StandardIngredient[]>(json);

            return ingredients.Intersect(deserializedIngredients, new IngredientComparer())
                .Count() == ingredients.Count();
        }
    }
}
