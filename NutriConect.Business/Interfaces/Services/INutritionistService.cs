using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Services;

public interface INutritionistService
{
    Task<NutritionistProfileViewModel?> GetProfileAsync(int userId);
    Task UpdateProfileAsync(int userId, UpdateNutritionistProfileInputModel input);
    Task<NutritionistPublicViewModel?> GetPublicProfileAsync(int nutritionistId, int? currentUserId = null);
    Task<List<NutritionistCardViewModel>> SearchAsync(string? query, NutritionistSpecialtyEnum? specialty, string? modality);
}
