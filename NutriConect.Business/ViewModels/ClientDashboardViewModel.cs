using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class ClientDashboardViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public ClientGoalEnum MainGoal { get; set; }

    public int UnreadMessages { get; set; }
    public List<ConversationListItemViewModel> RecentConversations { get; set; } = [];
    public List<RecipeCardViewModel> FeaturedRecipes { get; set; } = [];
    public List<TipCardViewModel> RecentTips { get; set; } = [];
}
