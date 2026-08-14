using NutriConect.Business.Enums;

namespace NutriConect.Business.Entities;

public class Nutritionist : User
{
    public string DisplayName { get; set; } = string.Empty;
    public string ProfessionalTitle { get; set; } = string.Empty;
    public NutritionistSpecialtyEnum? MainSpecialty { get; set; }
    public int YearsOfExperience { get; set; }
    public string? About { get; set; }
    public string CrnRegistration { get; set; } = string.Empty;
    public bool CrnVerified { get; set; }
    public decimal PricePerSession { get; set; }
    public bool AcceptsOnline { get; set; }
    public bool AcceptsInPerson { get; set; }
    public SessionDurationEnum SessionDurationMinutes { get; set; } = SessionDurationEnum.Cinquenta;
    public string? OfficeAddress { get; set; }
    public string? Specialties { get; set; }
    public string? PaymentMethods { get; set; }
    public bool AvailableForNewPatients { get; set; } = true;

    public ICollection<NutritionistCredential> Credentials { get; set; } = [];
}
