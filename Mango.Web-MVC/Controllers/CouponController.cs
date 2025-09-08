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
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? Couponlist = new();
            var response = await _couponService.GetAllCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                Couponlist = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            return View(Couponlist);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

       [HttpPost]
       public async Task<IActionResult> CouponCreate(CouponDto model)
       {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int CouponId)
        {
            var response = await _couponService.GetCouponById(CouponId);
            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto model)
        {
           

            var response = await _couponService.DeleteCouponAsync(model.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
           

            return View(model);
        }
    }
}
