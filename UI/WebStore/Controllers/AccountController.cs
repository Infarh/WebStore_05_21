using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Register

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Регистрация нового пользователя {0}", Model.UserName);

            var user = new User
            {
                UserName = Model.UserName
            };

            var register_result = await _UserManager.CreateAsync(user, Model.Password);
            if (register_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                await _UserManager.AddToRoleAsync(user, Role.Users);

                _Logger.LogInformation("Пользователю {0} назначена роль {1}", 
                    user.UserName, Role.Users);

                //await _UserManager.RemoveFromRoleAsync(user, Role.Administrators);

                await _SignInManager.SignInAsync(user, false);

                _Logger.LogInformation("Пользователь {0} автоматически вошёл в систему после регистрации", user.UserName);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in register_result.Errors)
                ModelState.AddModelError("", error.Description);

            _Logger.LogWarning("Ошибка при регистрации пользователя {0} в систему: {1}",
                Model.UserName,
                string.Join(", ", register_result.Errors.Select(err => err.Description)));

            return View(Model);
        } 

        #endregion

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password, 
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );
            
            if (login_result.Succeeded)
            {
                //return Redirect(Model.ReturnUrl); // не безопасно!
                //if (Url.IsLocalUrl(Model.ReturnUrl))
                //    return Redirect(Model.ReturnUrl);
                //else
                //    return RedirectToAction("Index", "Home");
                return LocalRedirect(Model.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "Ошибка в имени пользователя, либо в пароле");

            return View(Model);
        }

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
