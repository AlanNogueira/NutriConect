using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Mappings;
using System.Security.Claims;

namespace NutriConect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IHttpContextAccessor _contextAccessor;
        private UserManager<ApplicationUser> _userManager;

        public RecipeController(
            IRecipeService recipeService,
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _recipeService = recipeService;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        [HttpPost("/api/CreateRecipe")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> CreateRecipe(CreateRecipeInputModel createRecipe)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
                return BadRequest("Usuário da postagem não encontrado.");

            var recipe = RecipeMapping.CreateRecipeToRecipe(createRecipe, user);

            _recipeService.Add(recipe);

            return Ok("Receita criada com sucesso.");
        }
    }
}
