using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginInputModel input, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(input);

        var succeeded = await _authService.LoginAsync(input.Email, input.Password, input.RememberMe);

        if (!succeeded)
        {
            ModelState.AddModelError(string.Empty, "E-mail ou senha incorretos.");
            return View(input);
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterInputModel input)
    {
        if (!ModelState.IsValid)
            return View(input);

        var (succeeded, errors) = await _authService.RegisterAsync(input);

        if (!succeeded)
        {
            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            return View(input);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}
