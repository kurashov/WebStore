using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController( UserManager<User> userManager,
            SignInManager<User> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if( !ModelState.IsValid )
            {
                return View(model);
            }

            var user = new User( model.UserName );

            var registrationResult = await _userManager.CreateAsync(user, model.Password);
            if (registrationResult.Succeeded)
            {
                var addToRoleTask = _userManager.AddToRoleAsync( user, Role.User );
                var signInTask = _signInManager.SignInAsync(user, false);

                addToRoleTask.Wait();
                signInTask.Wait();

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registrationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public IActionResult Login( string returnUrl )
        {
            return View( new LoginViewModel
            {
                ReturnUrl = returnUrl
            } );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login( LoginViewModel model )
        {
            if( !ModelState.IsValid )
            {
                return View(model);
            }

            var login_result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false);

            if (login_result.Succeeded)
            {
                if( Url.IsLocalUrl( model.ReturnUrl ) )
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя, или пароль!");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}
