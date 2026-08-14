using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class NutritionistProfileViewModel
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;
    public string ProfessionalTitle { get; set; } = string.Empty;
    public NutritionistSpecialtyEnum? MainSpecialty { get; set; }
    public int YearsOfExperience { get; set; }
    public string? About { get; set; }
    public string CrnRegistration { get; set; } = string.Empty;
    public bool CrnVerified { get; set; }
    public List<NutritionistSpecialtyEnum> Specialties { get; set; } = [];
    public bool AvailableForNewPatients { get; set; }

    public bool AcceptsOnline { get; set; }
    public bool AcceptsInPerson { get; set; }
    public decimal PricePerSession { get; set; }
    public SessionDurationEnum SessionDurationMinutes { get; set; }
    public string? OfficeAddress { get; set; }
    public List<PaymentMethodEnum> PaymentMethods { get; set; } = [];

    public List<string> Credentials { get; set; } = [];

    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? City { get; set; }
    public DateOnly? BirthDate { get; set; }

    public string Initials => string.Concat(
        DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                   .Take(2)
                   .Select(w => char.ToUpper(w[0])));
}
