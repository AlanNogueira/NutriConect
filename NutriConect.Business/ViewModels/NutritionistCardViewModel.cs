using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class NutritionistCardViewModel
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string ProfessionalTitle { get; set; } = string.Empty;
    public NutritionistSpecialtyEnum? MainSpecialty { get; set; }
    public int YearsOfExperience { get; set; }
    public bool CrnVerified { get; set; }
    public bool AcceptsOnline { get; set; }
    public bool AcceptsInPerson { get; set; }
    public decimal PricePerSession { get; set; }
    public string? City { get; set; }
    public List<NutritionistSpecialtyEnum> Specialties { get; set; } = [];
    public bool AvailableForNewPatients { get; set; }
    public double? AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool TopRated => AverageRating >= 4.5 && ReviewCount >= 3;

    public string AverageDisplay =>
        AverageRating.HasValue
            ? AverageRating.Value.ToString("0.0", new System.Globalization.CultureInfo("pt-BR"))
            : string.Empty;

    public string Initials => string.Concat(
        DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                   .Take(2)
                   .Select(w => char.ToUpper(w[0])));
}
