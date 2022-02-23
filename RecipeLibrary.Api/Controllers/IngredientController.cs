using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RecipeLibrary.Core;
using RecipeLibrary.Models;

namespace RecipeLibrary.Api.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientLibrary _ingredientLibrary;

        public IngredientController(IIngredientLibrary ingredientLibrary)
        {
            _ingredientLibrary = ingredientLibrary;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string term = null)
        {
            await _ingredientLibrary.Load();

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(await _ingredientLibrary.Search(term));
            }

            return Ok(_ingredientLibrary.Ingredients);
        }

        [HttpPost]
        public async Task<IActionResult> Post(StandardIngredient ingredient)
        {
            _ingredientLibrary.AddIngredient(ingredient);
            await _ingredientLibrary.Save();

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Measurements()
        {
            _ingredientLibrary.MigrateMeasurements();
            return Ok();
        }
    }
}
