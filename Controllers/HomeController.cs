using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupClubWebApp.Helpers;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;
using RunGroupClubWebApp.ViewModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunGroupClubWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger,IClubRepository clubRepository)
        {
            _logger = logger;
            _clubRepository = clubRepository;
        }

        public async Task< IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeVm = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io/105.159.125.60?token=1f136264abd3ab";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo MyRJI = new RegionInfo(ipInfo.Country);
                ipInfo.Country = MyRJI.EnglishName;
                homeVm.City = ipInfo.City;
                homeVm.State = ipInfo.Region;
                if (homeVm.City != null)
                {
                    homeVm.clubs = await _clubRepository.GetClubsByCity(homeVm.City);
                }
                else
                {
                    homeVm.clubs = null;
                }
                return View(homeVm);

            }catch(Exception ex)
            {
                homeVm.clubs = null;
            }
 
            return View(homeVm);
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