using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Controllers;

[Authorize]
public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var profile = await _clientService.GetProfileAsync(userId);

        if (profile is null)
            return RedirectToAction("Login", "Account");

        var input = new UpdateClientProfileInputModel
        {
            FullName = profile.FullName,
            Phone = profile.Phone,
            City = profile.City,
            BirthDate = profile.BirthDate,
            MainGoal = profile.MainGoal,
            DietaryPreference = profile.DietaryPreference,
            About = profile.About
        };

        ViewBag.Initials = profile.Initials;
        return View(input);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(UpdateClientProfileInputModel input)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Initials = string.Concat(
                input.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                              .Take(2)
                              .Select(w => char.ToUpper(w[0])));
            return View(input);
        }

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _clientService.UpdateProfileAsync(userId, input);

        TempData["Success"] = "Perfil atualizado com sucesso.";
        return RedirectToAction(nameof(Profile));
    }
}
