using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RunGroupClubWebApp.Data;
using RunGroupClubWebApp.Models;
using RunGroupClubWebApp.ViewModel;

namespace RunGroupClubWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var PasswordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (PasswordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Club");
                    }
                }
                TempData["Error"] = "Ywrong credential. Please, try again";
                return View(loginVM);
            }
            TempData["Error"] = "Ywrong credential. Please, try again";

            return View(loginVM);
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var User = await _userManager.FindByEmailAsync(registerVM.Email);
            if (User != null)
            {
                TempData["Error"] = "This email was already founed";
                return View(registerVM);
            }
            var newUser = new AppUser()
            {
                Email = registerVM.Email,
                UserName = registerVM.Email
            };
            var NewUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (NewUserResponse.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Club");
        }
        [HttpPost]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
