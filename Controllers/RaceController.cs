using Microsoft.AspNetCore.Mvc;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;

namespace RunGroupClubWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetRaces();
            return View(races);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Race race=await _raceRepository.GetRaceByIdAsync(id);
            return View(race);
        }
    }
}
