using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Controllers;

[Authorize]
public class NutritionistController : Controller
{
    private readonly INutritionistService _nutritionistService;
    private readonly INutritionistReviewService _reviewService;
    private readonly UserManager<User> _userManager;

    public NutritionistController(
        INutritionistService nutritionistService,
        INutritionistReviewService reviewService,
        UserManager<User> userManager)
    {
        _nutritionistService = nutritionistService;
        _reviewService = reviewService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string? query, NutritionistSpecialtyEnum? specialty, string? modality)
    {
        var nutritionists = await _nutritionistService.SearchAsync(query, specialty, modality);

        return View(new NutritionistSearchViewModel
        {
            Nutritionists = nutritionists,
            Query = query,
            Specialty = specialty,
            Modality = modality,
            CanMessage = await _userManager.GetUserAsync(User) is Client
        });
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var profile = await _nutritionistService.GetProfileAsync(userId);

        if (profile is null)
            return RedirectToAction("Login", "Account");

        var input = new UpdateNutritionistProfileInputModel
        {
            DisplayName = profile.DisplayName,
            ProfessionalTitle = profile.ProfessionalTitle,
            MainSpecialty = profile.MainSpecialty,
            YearsOfExperience = profile.YearsOfExperience,
            About = profile.About,
            Specialties = profile.Specialties,
            AvailableForNewPatients = profile.AvailableForNewPatients,
            AcceptsOnline = profile.AcceptsOnline,
            AcceptsInPerson = profile.AcceptsInPerson,
            PricePerSession = profile.PricePerSession,
            SessionDurationMinutes = profile.SessionDurationMinutes,
            OfficeAddress = profile.OfficeAddress,
            PaymentMethods = profile.PaymentMethods,
            Credentials = profile.Credentials,
            FullName = profile.FullName,
            Phone = profile.Phone,
            City = profile.City,
            BirthDate = profile.BirthDate
        };

        ViewBag.Initials = profile.Initials;
        ViewBag.Email = profile.Email;
        ViewBag.CrnVerified = profile.CrnVerified;
        ViewBag.CrnRegistration = profile.CrnRegistration;
        ViewBag.NutritionistId = profile.Id;
        return View(input);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(UpdateNutritionistProfileInputModel input)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Initials = string.Concat(
                input.DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                 .Take(2)
                                 .Select(w => char.ToUpper(w[0])));
            return View(input);
        }

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _nutritionistService.UpdateProfileAsync(userId, input);

        TempData["Success"] = "Perfil atualizado com sucesso.";
        return RedirectToAction(nameof(Profile));
    }

    [HttpGet]
    public async Task<IActionResult> Public(int id)
    {
        var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var profile = await _nutritionistService.GetPublicProfileAsync(id, currentUserId);
        if (profile is null)
            return NotFound();

        return View(profile);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(CreateNutritionistReviewInputModel input)
    {
        var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!ModelState.IsValid)
            TempData["Error"] = "Escolha uma nota de 1 a 5 estrelas.";
        else if (!await _reviewService.CreateAsync(authorId, input))
            TempData["Error"] = "Não foi possível registrar sua avaliação. Você já avaliou este profissional ou não tem permissão.";
        else
            TempData["Success"] = "Avaliação enviada. Obrigado pelo feedback!";

        return RedirectToAction(nameof(Public), new { id = input.NutritionistId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReview(int reviewId, int nutritionistId)
    {
        var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _reviewService.DeleteAsync(reviewId, authorId);

        return RedirectToAction(nameof(Public), new { id = nutritionistId });
    }
}
