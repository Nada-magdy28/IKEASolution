using IKEA.DAL.Models.Identity;
using IKEA.PL.Views.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        #region Services
        public AccountController(UserManager<ApplicationUser> _userManager ,SignInManager<ApplicationUser>signInManager)
        {
            userManager = _userManager;
            this.signInManager = signInManager;
        }
        #endregion
        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var User = await userManager.FindByNameAsync(signUpViewModel.Email);
            if (User != null)
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This email already exists");
                return View(signUpViewModel);
            }
            User = new ApplicationUser()
            {
                UserName = signUpViewModel.UserName,
                Email = signUpViewModel.Email,
                FirstName = signUpViewModel.FirstName,
                LastName = signUpViewModel.LastName,
                IsAgree = signUpViewModel.IsAgreed.ToString(),

            };
            var result = await userManager.CreateAsync(User, signUpViewModel.Password);
            if (result.Succeeded)
            {
                //await userManager.AddToRoleAsync(User, "Employee");
                return RedirectToAction(nameof(Login));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(signUpViewModel);
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
          if(!ModelState.IsValid) 
                return BadRequest();
          var User= await userManager.FindByEmailAsync(loginViewModel.Email);
            if (User is not null)
            {
                var result = await signInManager.PasswordSignInAsync(User, loginViewModel.Password,loginViewModel.RememberMe ,false);
                if (result.IsNotAllowed)
                    ModelState.AddModelError(string.Empty, "Email is not confirmed");
                if (result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "User is locked out");
                if (result.Succeeded)
                
                    return RedirectToAction(nameof(HomeController.Index),"Home");
                
               



            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(loginViewModel);

        }
        #endregion
        #region signout
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}