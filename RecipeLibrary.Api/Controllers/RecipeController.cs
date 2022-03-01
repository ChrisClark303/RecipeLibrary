using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeLibrary.Core;
using RecipeLibrary.Models;

namespace RecipeLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IRecipeLibrary _library;

        public RecipeController(IRecipeLibrary library)
        {
            _library = library;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            await _library.Load();
            return Ok(_library.Recipes);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetRecipe(string recipeId)
        {
            var recipe = await _library.LoadById(recipeId);
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Recipe recipe)
        {
            _library.AddRecipe(recipe);
            await _library.Save();
            return Ok();
        }
    }
}