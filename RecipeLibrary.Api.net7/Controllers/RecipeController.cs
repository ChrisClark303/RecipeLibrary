using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeLibrary.Core.Models;
using RecipeLibrary.Core.Services;

namespace RecipeLibrary.Api.net7.Controllers
{
    [Route("v2")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("recipes")]
        public IActionResult AddRecipe(RecipeLight recipe)
        {
            _recipeService.AddRecipe(recipe);
            return Ok();
        }

        [HttpGet("recipes")]
        public async Task<IActionResult> GetRecipes()
        {
            return Ok(await _recipeService.GetRecipes());
        }

        [HttpGet("recipes/lucky-dip")]
        public async Task<IActionResult> RecipeLuckyDip()
        {
            return Ok(await _recipeService.GetRandomRecipe());
        }
    }
}
