using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.InputModels.Filters;
using NutriConect.Business.InputModels.Professional;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Mappings;
using NutriConect.Business.Services;
using System.Text;

namespace NutriConect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfessionalController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IProfessionalService _professionalService;

        public ProfessionalController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IProfessionalService professionalService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _professionalService = professionalService;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateProfessionalUser")]
        public async Task<IActionResult> CreateProfessionalUser([FromBody] CreateProfessionalInputModel createProfessional)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var professional = ProfessionalMapping.CreateProfessionalToProfessional(createProfessional);

            var result = await _userManager.CreateAsync(professional.User, createProfessional.Password);
            if (result.Errors.Any())
                return BadRequest(result.Errors);

            result = await _userManager.AddToRoleAsync(professional.User, "PROFESSIONAL");
            if (result.Errors.Any())
                return BadRequest(result.Errors);

            var userId = await _userManager.GetUserIdAsync(professional.User);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(professional.User);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultConfirmMail = await _userManager.ConfirmEmailAsync(professional.User, code);

            await _professionalService.Add(professional);

            if (resultConfirmMail.Succeeded)
                return Ok("Usuário criado com sucesso.");
            else
                return BadRequest("Erro ao confirmar usuário.");
        }

        [Produces("application/json")]
        [HttpPut("/api/UpdateProfessional")]
        public async Task<IActionResult> UpdateProfessional([FromBody] UpdateProfessionalInputModel updateProfessional)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var professional = await _professionalService.FindByIdTracked(updateProfessional.Id);
            if (professional == null) return BadRequest("Profissional não encontrado.");

            ProfessionalMapping.UpdateProfessionalToProfessional(updateProfessional, professional);
            await _professionalService.Update(professional);

            return Ok("Usuário atualizado com sucesso.");
        }

        [Produces("application/json")]
        [HttpGet("/api/GetProfessionalByEmail/{email}")]
        public async Task<IActionResult> GetProfessionalByEmail([FromRoute] string email)
        {
            var professional = await _professionalService.GetProfessionalByEmail(email);
            if (professional == null) return BadRequest("Profissional não encontrado");

            var professionalViewModel = ProfessionalMapping.ProfessionalToViewModel(professional);

            return Ok(professionalViewModel);
        }

        [Produces("application/json")]
        [HttpGet("/api/GetProfessionals")]
        public async Task<IActionResult> GetProfessionals([FromQuery]ProfessionalFilters filters, int page = 1, int pageSize = 10)
        {
            var professionals = await _professionalService.GetProfessionals(filters, page, pageSize);
            return Ok(professionals);
        }
    }
}
