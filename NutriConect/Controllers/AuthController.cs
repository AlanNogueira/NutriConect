using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Token;

namespace NutriConect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("/api/CreateToken")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateToken([FromBody] LoginInputModel login)
        {
            if(!ModelState.IsValid) return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%"))
                    .AddSubject("NutriConect")
                    .AddIssuer("NutriConect.Security.Bearer")
                    .AddAudience("NutriConect.Security.Bearer")
                    .AddClaim("email", login.Email)
                    .AddExpiry(5)
                    .Builder();

                var user = await _userManager.FindByEmailAsync(login.Email);
                var role = await _userManager.GetRolesAsync(user);

                return Ok(new { token = token.value, userName = user.UserName, role = role.FirstOrDefault() });
            }
            else
                return Unauthorized("Usuário ou senha incorretos.");
        }

        [Produces("application/json")]
        [HttpPost("/api/UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordInputModel updatePassword)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(updatePassword.Email);
            if (user == null) return BadRequest("Usuário não encontrado.");

            var result = await _userManager.ChangePasswordAsync(user, updatePassword.CurrentPassword, updatePassword.NewPassword);
            if (result.Errors.Any()) return BadRequest(result.Errors);

            return Ok("Senha atualizada com sucesso.");
        }
    }
}
