using Mango.Web_MVC.Models;
using Mango.Web_MVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Mango.Web_MVC.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {
            List<CouponDto>? Couponlist = new();
            var response = await _couponService.GetAllCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                Couponlist = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            return View(Couponlist);
        }
    }
}
