using System.ComponentModel.DataAnnotations;
using NutriConect.Business.Enums;

namespace NutriConect.Business.InputModels;

public class UpdateNutritionistProfileInputModel
{
    [Required(ErrorMessage = "O nome de exibição é obrigatório.")]
    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string ProfessionalTitle { get; set; } = string.Empty;

    public NutritionistSpecialtyEnum? MainSpecialty { get; set; }

    [Range(0, 60, ErrorMessage = "Informe um valor entre 0 e 60 anos.")]
    public int YearsOfExperience { get; set; }

    [MaxLength(1000)]
    public string? About { get; set; }

    public List<NutritionistSpecialtyEnum> Specialties { get; set; } = [];

    public bool AcceptsOnline { get; set; }
    public bool AcceptsInPerson { get; set; }

    [Range(0, 99999, ErrorMessage = "Informe um valor de consulta válido.")]
    public decimal PricePerSession { get; set; }

    public SessionDurationEnum SessionDurationMinutes { get; set; } = SessionDurationEnum.Cinquenta;

    [MaxLength(200)]
    public string? OfficeAddress { get; set; }

    public List<PaymentMethodEnum> PaymentMethods { get; set; } = [];

    public bool AvailableForNewPatients { get; set; }

    public List<string> Credentials { get; set; } = [];

    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(120)]
    public string? City { get; set; }

    public DateOnly? BirthDate { get; set; }
}
