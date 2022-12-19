using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupClubWebApp.Data;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;
using RunGroupClubWebApp.ViewModel;

namespace RunGroupClubWebApp.Controllers
{
    public class ClubController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(/*ApplicationDbContext db,*/ IClubRepository clubRepository,IPhotoService photoService,IHttpContextAccessor httpContextAccessor)
        {
            //_db = db;
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetClubs();
            return View(clubs);
        }
        // detail Action Method
        public async Task<IActionResult> Detail(int id)
        {
            Club club =await _clubRepository.GetClubByIdAsync(id);
            return View(club);
        }
        // Create Get
        [HttpGet]
        public IActionResult Create()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubVM = new CreateClubViewModel
            {
                AppUserId = curUser
            };
            return View(createClubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVm.Image);
                var club = new Club
                {
                    Title = clubVm.Title,
                    Description = clubVm.Description,
                    Image = result.Url.ToString(),
                    AppUserId=clubVm.AppUserId,
                    Address=new Address
                    {
                        Street=clubVm.Address.Street,
                        City=clubVm.Address.City,
                        State=clubVm.Address.State,

                    }

                };
                      _clubRepository.Add(club);
                      return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Error");
            }
            return View(clubVm);

        }

        // Update Get
        public async Task<IActionResult> Edit(int id)
        {
            Club club = await _clubRepository.GetClubByIdAsync(id);
            if(club==null) return View("Error");
            var ClubVm = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AdressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(ClubVm);
            
        }

        //Update Post
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel ClubVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", ClubVm);
            }
            var userClub = await _clubRepository.GetClubByIdAsyncNoTraking(id);
            if (userClub != null)
            {
                try
                {
                await _photoService.DeletePhotoAsync(userClub.Image);

                }catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(ClubVm);
                }
                var photoResult = await _photoService.AddPhotoAsync(ClubVm.Image);
                Club club = new Club
                {
                    Id = id,
                    Title = ClubVm.Title,
                    Description = ClubVm.Description,
                    Image = photoResult.Url.ToString(),
                    AdressId = ClubVm.AddressId,
                    Address = ClubVm.Address,
                };

                _clubRepository.Update(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(ClubVm);
            }
        }
    }
}
