using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Controllers;

public class RecipeController : Controller
{
    private readonly IRecipeService _recipeService;
    private readonly IRecipeReviewService _reviewService;

    public RecipeController(IRecipeService recipeService, IRecipeReviewService reviewService)
    {
        _recipeService = recipeService;
        _reviewService = reviewService;
    }

    private int? CurrentUserId =>
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

    [HttpGet]
    public async Task<IActionResult> Index(RecipeCategoryEnum? category, string? query, RecipeSortEnum sort = RecipeSortEnum.Recentes)
    {
        var model = await _recipeService.ListAsync(category, query, sort);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var recipe = await _recipeService.GetByIdAsync(id, CurrentUserId);
        if (recipe is null) return NotFound();
        return View(recipe);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create() => View(new RecipeInputModel());

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecipeInputModel input)
    {
        CleanLists(input);
        if (!ModelState.IsValid) return View(input);

        var id = await _recipeService.CreateAsync(CurrentUserId!.Value, input);
        TempData["Success"] = "Receita publicada com sucesso.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var input = await _recipeService.GetForEditAsync(id, CurrentUserId!.Value);
        if (input is null) return NotFound();

        ViewBag.RecipeId = id;
        return View(input);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RecipeInputModel input)
    {
        CleanLists(input);
        if (!ModelState.IsValid)
        {
            ViewBag.RecipeId = id;
            return View(input);
        }

        var ok = await _recipeService.UpdateAsync(id, CurrentUserId!.Value, input);
        if (!ok) return NotFound();

        TempData["Success"] = "Receita atualizada com sucesso.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _recipeService.DeleteAsync(id, CurrentUserId!.Value);
        if (!ok) return NotFound();

        TempData["Success"] = "Receita excluída.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(CreateRecipeReviewInputModel input)
    {
        var ok = await _reviewService.CreateAsync(CurrentUserId!.Value, input);
        TempData[ok ? "Success" : "Error"] = ok
            ? "Avaliação publicada com sucesso."
            : "Não foi possível registrar sua avaliação.";
        return RedirectToAction(nameof(Details), new { id = input.RecipeId });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReview(int reviewId, int recipeId)
    {
        var ok = await _reviewService.DeleteAsync(reviewId, CurrentUserId!.Value);
        TempData[ok ? "Success" : "Error"] = ok
            ? "Avaliação excluída."
            : "Não foi possível excluir a avaliação.";
        return RedirectToAction(nameof(Details), new { id = recipeId });
    }

    private static void CleanLists(RecipeInputModel input)
    {
        input.Ingredients = input.Ingredients
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim())
            .ToList();

        input.Steps = input.Steps
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim())
            .ToList();
    }
}
