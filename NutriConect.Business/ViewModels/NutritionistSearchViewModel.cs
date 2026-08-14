using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class NutritionistSearchViewModel
{
    public List<NutritionistCardViewModel> Nutritionists { get; set; } = [];
    public string? Query { get; set; }
    public NutritionistSpecialtyEnum? Specialty { get; set; }
    public string? Modality { get; set; }
    public int Total => Nutritionists.Count;
    public bool CanMessage { get; set; }
}
