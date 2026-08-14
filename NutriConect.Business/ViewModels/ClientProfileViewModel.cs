using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class ClientProfileViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? City { get; set; }
    public DateOnly? BirthDate { get; set; }
    public ClientGoalEnum MainGoal { get; set; }
    public DietaryPreferenceEnum DietaryPreference { get; set; }
    public string? About { get; set; }

    public string Initials => string.Concat(
        FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(2)
                .Select(w => char.ToUpper(w[0])));
}
