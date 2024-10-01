using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Mappings;
using System.Text;

namespace NutriConect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IClientService _clientService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IClientService clientService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clientService = clientService;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateClientUser")]
        public async Task<IActionResult> CreateClientUser([FromBody] CreateClientUserInputModel createClient)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var client = ClientMapping.CreateClientToClient(createClient);

            var result = await _userManager.CreateAsync(client.User, createClient.Password);
            if (result.Errors.Any())
                return BadRequest(result.Errors);

            result = await _userManager.AddToRoleAsync(client.User, "CLIENT");
            if (result.Errors.Any())
                return BadRequest(result.Errors);

            var userId = await _userManager.GetUserIdAsync(client.User);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(client.User);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultConfirmMail = await _userManager.ConfirmEmailAsync(client.User, code);

            await _clientService.Add(client);

            if (resultConfirmMail.Succeeded)
                return Ok("Usuário criado com sucesso.");
            else
                return BadRequest("Erro ao confirmar usuário.");
        }
    }
}
