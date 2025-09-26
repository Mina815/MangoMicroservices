using Mango.Web_MVC.Models;
using Mango.Web_MVC.Services.IServices;
using Mango.Web_MVC.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web_MVC.Controllers
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
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticData.RoleAdmin,Value=StaticData.RoleAdmin},
                new SelectListItem{Text=StaticData.RoleCustomer,Value=StaticData.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            var result = await _authService.RegisterAsync(obj);
            ResponseDto? assingRole;

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = StaticData.RoleCustomer;
                }
                assingRole = await _authService.AssignRole(obj);
                if (assingRole != null && assingRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=StaticData.RoleAdmin,Value=StaticData.RoleAdmin},
                new SelectListItem{Text=StaticData.RoleCustomer,Value=StaticData.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(obj);
        }
    }
}
