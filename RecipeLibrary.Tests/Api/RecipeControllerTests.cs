using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeLibrary.Api.Controllers;
using RecipeLibrary.Core;
using RecipeLibrary.Models;
using System.Threading.Tasks;
using Xunit;

namespace RecipeLibrary.Tests.Api
{
    public class RecipeControllerTests
    {
        private RecipeController CreateController(Mock<IRecipeLibrary> mockLibrary = null)
        {
            return new RecipeController((mockLibrary ?? new Mock<IRecipeLibrary>()).Object);
        }

        [Fact]
        public async Task Get_CallsRecipeLibrary_Load()
        {
            var library = new Mock<IRecipeLibrary>();
            var controller = CreateController(library);

            await controller.Get();

            library.Verify(lib => lib.Load());
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            var library = new Mock<IRecipeLibrary>();
            var controller = CreateController(library);

            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithRecipes_FromLibrary()
        {
            var recipes = new Recipe[0];
            var library = new Mock<IRecipeLibrary>();
            library.SetupGet(lib => lib.Recipes)
                .Returns(recipes);
            var controller = CreateController(library);

            var result = await controller.Get();

            var objResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(recipes, objResult.Value);
        }

        [Fact]
        public async Task Post_CallsRecipeLibrary_AddRecipe()
        {
            var library = new Mock<IRecipeLibrary>();
            var controller = CreateController(library);

            await controller.Post(new Recipe());

            library.Verify(lib => lib.AddRecipe(It.IsAny<Recipe>()));
        }

        [Fact]
        public async Task Post_CallsRecipeLibrary_AddRecipe_WithRecipe()
        {
            var recipe = new Recipe();
            var library = new Mock<IRecipeLibrary>();
            var controller = CreateController(library);

            await controller.Post(recipe);

            library.Verify(lib => lib.AddRecipe(recipe));
        }

        [Fact]
        public async Task Post_CallsIngredientLibrary_Save()
        {
            var library = new Mock<IRecipeLibrary>();
            var controller = CreateController(library);

            await controller.Post(new Recipe());

            library.Verify(lib => lib.Save());
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            var library = new Mock<IRecipeLibrary>();
            var controller = CreateController(library);

            var result = await controller.Post(new Recipe());

            Assert.IsType<OkResult>(result);
        }
    }
}
