using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Services.LoginService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EcommerceApp.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var activeUser = await _loginService.Login(loginDTO);
            
            if (activeUser != null)
            {
                var jsonUser = JsonConvert.SerializeObject(activeUser);
                HttpContext.Session.SetString("activeUser", jsonUser);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role,activeUser.Roles.ToString())); 
                var userIdentity=new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principle= new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                if (activeUser.Roles==Domain.Enums.Roles.Admin)
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
                
            }
            return View(loginDTO);
        }
    }
}
