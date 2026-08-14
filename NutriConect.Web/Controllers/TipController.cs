using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Controllers;

public class TipController : Controller
{
    private readonly ITipService _tipService;

    public TipController(ITipService tipService) => _tipService = tipService;

    private int? CurrentUserId =>
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

    [HttpGet]
    public async Task<IActionResult> Index(TipCategoryEnum? category, bool onlyProfessionals)
    {
        var model = await _tipService.ListAsync(category, onlyProfessionals, CurrentUserId);
        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTipInputModel input)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Escreva sua dica antes de compartilhar.";
            return RedirectToAction(nameof(Index));
        }

        await _tipService.CreateAsync(CurrentUserId!.Value, input);
        TempData["Success"] = "Dica compartilhada com a comunidade.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Comment(CreateTipCommentInputModel input)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Escreva um comentário.";
            return RedirectToAction(nameof(Index));
        }

        var ok = await _tipService.AddCommentAsync(CurrentUserId!.Value, input);
        if (!ok) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _tipService.DeleteAsync(id, CurrentUserId!.Value);
        if (!ok) return NotFound();

        TempData["Success"] = "Dica excluída.";
        return RedirectToAction(nameof(Index));
    }
}
