using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Models;
using RunGroupClubWebApp.ViewModel;

namespace RunGroupClubWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService)
        {
           _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        private void MapUserEdit(AppUser user,EditUserProfileViewModel editUserProfileVM,ImageUploadResult photoResult)
        {
            user.Id = editUserProfileVM.id;
            user.Pace = editUserProfileVM.Pace;
            user.MileAge = editUserProfileVM.Mileage;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editUserProfileVM.City;
            user.State = editUserProfileVM.State;

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

        //
        [HttpGet]
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            EditUserProfileViewModel editUserProfileVM = new EditUserProfileViewModel()
            {
                id = user.Id,
                Pace = user.Pace,
                Mileage = user.MileAge,
                City = user.City,
                State = user.State,
                ImageProfileUrl = user.ProfileImageUrl
            };
            return View(editUserProfileVM);

        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editUserProfileVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editUserProfileVM);
            }
            var user = await _dashboardRepository.GetByIdNoTracking(editUserProfileVM.id);
            if(user.ProfileImageUrl=="" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserProfileVM.Image);
                MapUserEdit(user, editUserProfileVM, photoResult);
                _dashboardRepository.Update(user);
               return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editUserProfileVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editUserProfileVM.Image);
               
                MapUserEdit(user, editUserProfileVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");

            }
        }
    }
}
