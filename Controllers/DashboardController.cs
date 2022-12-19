using Microsoft.AspNetCore.Mvc;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.ViewModel;

namespace RunGroupClubWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
           _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardVM = new DashboardViewModel()
            {
                Clubs = userClubs

            };
            return View(dashboardVM);
        }
    }
}
