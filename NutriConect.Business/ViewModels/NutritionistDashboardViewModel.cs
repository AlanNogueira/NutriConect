using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class NutritionistDashboardViewModel
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public NutritionistSpecialtyEnum? MainSpecialty { get; set; }
    public bool AvailableForNewPatients { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int PublishedRecipes { get; set; }
    public int PublishedTips { get; set; }
    public int UnreadMessages { get; set; }

    public List<NutritionistReviewViewModel> RecentReviews { get; set; } = [];
    public List<ConversationListItemViewModel> RecentConversations { get; set; } = [];

    public string AverageDisplay => AverageRating.ToString("0.0");
}
