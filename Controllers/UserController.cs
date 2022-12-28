using Microsoft.AspNetCore.Mvc;
using RunGroupClubWebApp.Interfaces;
using RunGroupClubWebApp.Repository;
using RunGroupClubWebApp.ViewModel;

namespace RunGroupClubWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRespository _userRespository;

        public UserController(IUserRespository userRespository)
        {
            
            _userRespository = userRespository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRespository.GetUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.MileAge
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        // Detail
        public async Task<IActionResult> Detail(string id)
        {
            var user =await _userRespository.GetUserById(id);
            var detailUserVM = new UserDetailViewModel()
            {
                id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.MileAge
            };
            return View(detailUserVM);
        }
    }
}
