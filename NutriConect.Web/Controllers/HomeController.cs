using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Models;
using System.Diagnostics;

namespace NutriConect.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IDashboardService _dashboardService;

        public HomeController(UserManager<User> userManager, IDashboardService dashboardService)
        {
            _userManager = userManager;
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated != true)
                return View();

            var user = await _userManager.GetUserAsync(User);
            if (user is null)
                return View();

            ViewData["ActiveNav"] = "inicio";

            if (user is Nutritionist)
            {
                var dashboard = await _dashboardService.GetNutritionistDashboardAsync(user.Id);
                if (dashboard is null) return View();
                return View("NutritionistDashboard", dashboard);
            }

            var clientDashboard = await _dashboardService.GetClientDashboardAsync(user.Id);
            if (clientDashboard is null) return View();
            return View("ClientDashboard", clientDashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
