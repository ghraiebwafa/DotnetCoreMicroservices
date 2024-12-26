using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roleListe = new List<SelectListItem>()
            {
                new SelectListItem { Text = StaticData.RoleAdmin, Value = StaticData.RoleAdmin },
                new SelectListItem { Text = StaticData.RoleCustomer, Value = StaticData.RoleCustomer },
                
            };
            ViewBag.RoleListe = roleListe;
            return View();
        }
        public IActionResult Logout()
        {
            //RegistrationDto registrationDto = new();
            return View();
        }
    

}
}
