using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeLibrary.Api.Controllers;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using System.Threading.Tasks;
using Xunit;

namespace RecipeLibrary.Tests.Api
{
    public class IngredientControllerTests
    {
        private IngredientController CreateController(Mock<IIngredientLibrary> mockLibrary = null)
        {
            return new IngredientController((mockLibrary ?? new Mock<IIngredientLibrary>()).Object);
        }

        [Fact]
        public async Task Get_CallsIngredientLibrary_Load()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            await controller.Get();

            library.Verify(lib => lib.Load());
        }

        [Fact]
        public async Task Get_WithSearchTerm_CallsIngredientLibrary_Search_With_Term()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            await controller.Get("Tomato");

            library.Verify(lib => lib.Search("Tomato"));
        }

        [Fact]
        public async Task Get_NoSearchTerm_CallsIngredientLibrary_Search_With_Term()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            await controller.Get();

            library.Verify(lib => lib.Search(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_NoSearchTerm_ReturnsOkResult_WithIngredients_FromLibrary()
        {
            var ingredients = new[]
                {
                    new StandardIngredient()
                };
            var library = new Mock<IIngredientLibrary>();
            library.SetupGet(lib => lib.Ingredients)
                .Returns(ingredients);
            var controller = CreateController(library);

            var result = await controller.Get();

            var objResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ingredients, objResult.Value);
        }

        [Fact]
        public async Task Get_WithSearchTerm_ReturnsOkResult_WithIngredients_FromLibrary()
        {
            var ingredients = new[]
                {
                    new StandardIngredient()
                };
            var library = new Mock<IIngredientLibrary>();
            library.Setup(lib => lib.Search(It.IsAny<string>()))
                .ReturnsAsync(ingredients);
            var controller = CreateController(library);

            var result = await controller.Get("Tomato");

            var objResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ingredients, objResult.Value);
        }

        [Fact]
        public async Task Post_CallsIngredientLibrary_AddIngredient()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            await controller.Post(new StandardIngredient());

            library.Verify(lib => lib.AddIngredient(It.IsAny<StandardIngredient>()));
        }

        [Fact]
        public async Task Post_CallsIngredientLibrary_AddIngredient_WithIngredient()
        {
            var ingredient = new StandardIngredient();
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            await controller.Post(ingredient);

            library.Verify(lib => lib.AddIngredient(ingredient));
        }

        [Fact]
        public async Task Post_CallsIngredientLibrary_Save()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            await controller.Post(new StandardIngredient());

            library.Verify(lib => lib.Save());
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            var library = new Mock<IIngredientLibrary>();
            var controller = CreateController(library);

            var result = await controller.Post(new StandardIngredient());

            Assert.IsType<OkResult>(result);
        }
    }
}
