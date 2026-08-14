using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class NutritionistPublicViewModel
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string ProfessionalTitle { get; set; } = string.Empty;
    public NutritionistSpecialtyEnum? MainSpecialty { get; set; }
    public int YearsOfExperience { get; set; }
    public string? About { get; set; }
    public string CrnRegistration { get; set; } = string.Empty;
    public bool CrnVerified { get; set; }
    public bool AcceptsOnline { get; set; }
    public bool AcceptsInPerson { get; set; }
    public decimal PricePerSession { get; set; }
    public SessionDurationEnum SessionDurationMinutes { get; set; }
    public string? City { get; set; }
    public List<NutritionistSpecialtyEnum> Specialties { get; set; } = [];
    public List<string> Credentials { get; set; } = [];

    public double? AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public List<RatingBucketViewModel> Distribution { get; set; } = [];
    public List<NutritionistReviewViewModel> Reviews { get; set; } = [];

    public bool CanReview { get; set; }

    public bool CanMessage { get; set; }

    public bool AlreadyReviewed { get; set; }

    public string AverageDisplay =>
        AverageRating.HasValue
            ? AverageRating.Value.ToString("0.0", new System.Globalization.CultureInfo("pt-BR"))
            : string.Empty;

    public string Initials => string.Concat(
        DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                   .Take(2)
                   .Select(w => char.ToUpper(w[0])));

    public string Modality =>
        (AcceptsOnline, AcceptsInPerson) switch
        {
            (true, true) => "Online / Presencial",
            (true, false) => "Online",
            (false, true) => "Presencial",
            _ => "—"
        };
}
