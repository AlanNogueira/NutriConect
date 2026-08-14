using System.ComponentModel.DataAnnotations;
using NutriConect.Business.Enums;

namespace NutriConect.Business.InputModels;

public class UpdateClientProfileInputModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(120)]
    public string? City { get; set; }

    public DateOnly? BirthDate { get; set; }

    public ClientGoalEnum MainGoal { get; set; } = ClientGoalEnum.ReeducacaoAlimentar;

    public DietaryPreferenceEnum DietaryPreference { get; set; } = DietaryPreferenceEnum.Onivora;

    [MaxLength(1000)]
    public string? About { get; set; }
}
