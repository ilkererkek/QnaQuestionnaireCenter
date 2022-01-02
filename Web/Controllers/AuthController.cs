using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Web.Models;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Login(model.Email, model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(type: ClaimTypes.Sid, value: user.Id.ToString()));
                    claims.Add(new Claim(type: ClaimTypes.Name, value: user.Name +" "+ user.Surname));
                    claims.Add(new Claim(type: ClaimTypes.MobilePhone, value: user.PhoneNumber));
                    claims.Add(new Claim(type: ClaimTypes.Email, value: user.Email));
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/");
                }
            }
            ViewBag.Error = "Hata";
            return View(new LoginViewModel());
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var flag = _userService.Register(model.ToUser(),model.Password);
                if (flag)
                {
                    var m = new LoginViewModel();
                    m.Email = model.Email;
                    m.Password = "";
                    return View("Login", m);
                }
            }
            ViewBag.Error = "Hata";
            model = new RegisterViewModel();
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);

            var model = new LoginViewModel();
            return View("Login",model);
        }
    }
}
