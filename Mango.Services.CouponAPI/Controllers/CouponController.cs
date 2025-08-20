using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _responseDto;
        public CouponController(AppDBContext db)
        {
            _db = db;
            _responseDto = new ResponseDto();
        }
        [HttpGet("{id}")]
        public ResponseDto GetCoupon(int id)
        {
            try
            {

                var coupon = _db.Coupons.FirstOrDefault(c => c.Id == id);
                if (coupon == null)
                {
                    _responseDto.Message = "No coupons found.";
                }
                var returnedCoupon =  new CouponDto
                {
                    CouponCode = coupon.CouponCode,
                    DiscountAmount = coupon.DiscountAmount,
                    CreatedDate = coupon.CreatedDate,
                    ExpiryDate = coupon.ExpiryDate,
                    IsActive = coupon.IsActive
                };
                _responseDto.Result = returnedCoupon;
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpGet("GetAllCoupons")]
        public ResponseDto GetAllCoupons()
        {
            var coupons = _db.Coupons.ToList();
            _responseDto.Result = coupons;
            if (coupons == null || !coupons.Any())
            {
                _responseDto.Message = "No coupons found.";
            }
            _responseDto.IsSuccess = true;
            return _responseDto;
        }
    }
}
