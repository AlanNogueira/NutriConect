using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.InputModels.Recipe;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Mappings;
using NutriConect.Business.Services;
using System.Security.Claims;

namespace NutriConect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IClientService _clientService;
        private readonly IProfessionalService _professionalService;
        private readonly IHttpContextAccessor _contextAccessor;
        private UserManager<ApplicationUser> _userManager;

        public RecipeController(
            IRecipeService recipeService,
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager,
            IClientService clientService,
            IProfessionalService professionalService)
        {
            _recipeService = recipeService;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _clientService = clientService;
            _professionalService = professionalService;
        }

        [HttpPost("/api/CreateRecipe")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateRecipe(CreateRecipeInputModel createRecipe)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var role = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            Client client = null;
            Professional professional = null;

            if (role == "Professional")
                professional = await _professionalService.GetProfessionalByEmail(email);
            else 
                client  = await _clientService.GetClientByEmail(email);

            var recipe = RecipeMapping.CreateRecipeToRecipe(createRecipe, client, professional);

            await _recipeService.Add(recipe);

            return Ok("Receita criada com sucesso.");
        }

        [HttpGet("/api/GetRecipeById/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeById(id);
            if(recipe is null) return NotFound();

            var recipeViewModel = RecipeMapping.RecipeToViewModel(recipe);

            return Ok(recipeViewModel);
        }

        [HttpGet("/api/GetRecipes")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRecipes([FromQuery]RecipeFilters filters, int page = 1, int pageSize = 10)
        {
            var recipes = await _recipeService.GetRecipes(filters, page, pageSize);

            var recipesViewModel = recipes.Select(x => RecipeMapping.RecipeToViewModel(x)).ToList();

            return Ok(recipesViewModel);
        }

        [HttpPost("api/CreateRecipeEvaluation")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateRecipeEvaluation([FromBody] RecipeEvaluationsInputModel createRecipeEvaluations)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var recipe = await _recipeService.GetRecipeById(createRecipeEvaluations.RecipeId);
            if (recipe is null) return NotFound("A receita informada não existe.");

            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var role = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            Client client = null;
            Professional professional = null;

            if (role == "Professional")
                professional = await _professionalService.GetProfessionalByEmail(email);
            else
                client = await _clientService.GetClientByEmail(email);

            var recipeEvaluation = RecipeMapping.CreateRecipeEvaluationToRecipeEvaluation(createRecipeEvaluations, recipe, client, professional);

            await _recipeService.CreateRecipeEvaluation(recipeEvaluation);

            return Ok("Receita avaliada com sucesso.");
        }

        [HttpGet("api/GetRecipeByUser")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRecipesByUser(int page = 1, int pageSize = 10)
        {
            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var recipes = await _recipeService.GetRecipesByUser(email, page, pageSize);

            var viewModel = recipes.Select(x => RecipeMapping.RecipeToReciberByUserViewModel(x)).ToList();

            return Ok(viewModel);
        }

        [HttpPut("/api/UpdateRecipe")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeInputMode updateRecipe)
        {
            var recipe = await _recipeService.GetRecipeByIdTracked(updateRecipe.Id);
            if (recipe is null) return NotFound("Receita não encontrada.");

            RecipeMapping.UpdateRecipeToRecipe(updateRecipe, recipe);
            await _recipeService.UpdateRecipe(recipe);

            return Ok(updateRecipe);
        }
    }
}
