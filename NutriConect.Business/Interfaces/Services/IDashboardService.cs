using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Services;

public interface IDashboardService
{
    Task<ClientDashboardViewModel?> GetClientDashboardAsync(int userId);
    Task<NutritionistDashboardViewModel?> GetNutritionistDashboardAsync(int userId);
}
