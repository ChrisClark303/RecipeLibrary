using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeLibrary.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("error")]
        public IActionResult Error()
        {
            var problem = Problem();
            return problem;
        }
    }
}