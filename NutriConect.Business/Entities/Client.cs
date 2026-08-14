using NutriConect.Business.Enums;

namespace NutriConect.Business.Entities;

public class Client : User
{
    public ClientGoalEnum MainGoal { get; set; } = ClientGoalEnum.ReeducacaoAlimentar;
    public DietaryPreferenceEnum DietaryPreference { get; set; } = DietaryPreferenceEnum.Onivora;
    public string? About { get; set; }
}
