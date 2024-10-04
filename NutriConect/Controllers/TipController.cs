using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.InputModels.Recipe;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Mappings;
using NutriConect.Business.Services;
using System.Security.Claims;

namespace NutriConect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITipService _tipService;
        private readonly IProfessionalService _professionalService;
        private readonly IClientService _clientService;

        public TipController(
            IHttpContextAccessor contextAccessor,
            ITipService tipService,
            IProfessionalService professionalService,
            IClientService clientService)
        {
            _contextAccessor = contextAccessor;
            _tipService = tipService;
            _professionalService = professionalService;
            _clientService = clientService;
        }

        [HttpPost("/api/CreateTip")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateTip([FromBody] CreateTipInputModel tip)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var role = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            Client client = null;
            Professional professional = null;

            if (role == "Professional")
                professional = await _professionalService.GetProfessionalByEmail(email);
            else
                client = await _clientService.GetClientByEmail(email);

            var recipe = TipMapping.CreateTipToTip(tip, client, professional);

            await _tipService.Add(recipe);

            return Ok("Dica criada com sucesso.");
        }

        [HttpGet("/api/GetTips")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTips([FromQuery]TipFilters filters, int page = 1, int pageSize = 10)
        {
            var tips = await _tipService.GetTips(filters, page, pageSize);

            var tipsViewModels = tips.Select(x => TipMapping.TipToViewModel(x)).ToList();

            return Ok(tipsViewModels);
        }

        [HttpGet("/api/GetTipById/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTipById(int id)
        {
            var tip = await _tipService.GetTipById(id);
            if (tip is null) return NotFound("Dica não encontrada.");

            var tipViewModel = TipMapping.TipToViewModel(tip);

            return Ok(tip);
        }

        [HttpPost("api/CreateTipEvaluation")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateTipEvaluation([FromBody] TipEvaluationInputModel tipEvaluationInputModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var tip = await _tipService.GetTipById(tipEvaluationInputModel.RecipeId);
            if (tip is null) return NotFound("A receita informada não existe.");

            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var role = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            Client client = null;
            Professional professional = null;

            if (role == "Professional")
                professional = await _professionalService.GetProfessionalByEmail(email);
            else
                client = await _clientService.GetClientByEmail(email);

            var tipEvaluation = TipMapping.CreateTipEvaluationToTipEvaluation(tipEvaluationInputModel, tip, client, professional);

            await _tipService.CreateTipEvaluation(tipEvaluation);

            return Ok("Receita avaliada com sucesso.");
        }

        [HttpGet("/api/GetTipsByUser")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTipsByUser(int page = 1, int pageSize = 10)
        {
            var email = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var tips = await _tipService.GetTipsByUser(email, page, pageSize);

            var viewModel = tips.Select(x => TipMapping.TipToTipByUserViewModel(x)).ToList();

            return Ok(viewModel);
        }

        [HttpPut("/api/UpdateTip")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateTip([FromBody] UpdateTipInputMode updateTip)
        {
            var tip = await _tipService.GetTipByIdTracked(updateTip.Id);
            if (tip is null) return NotFound("Dica não encontrada.");

            TipMapping.UpdateTipToTip(updateTip, tip);
            await _tipService.UpdateTip(tip);

            return Ok(updateTip);
        }
    }
}
