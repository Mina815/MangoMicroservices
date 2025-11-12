using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        public CouponController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
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

                var returnedCoupon = _mapper.Map<CouponDto>(coupon);
                _responseDto.Result = returnedCoupon;
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpGet("GetCouponByCode/{CouponCode}")]
        public ResponseDto GetCouponByCode(string CouponCode)
        {
            try
            {
                if (string.IsNullOrEmpty(CouponCode))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon code cannot be null or empty.";
                    return _responseDto;
                }
                var coupon = _db.Coupons.LastOrDefault(c => c.CouponCode == CouponCode);
                if (coupon == null)
                {
                    _responseDto.Message = "No coupons found.";
                }

                var returnedCoupon = _mapper.Map<CouponDto>(coupon);
                _responseDto.Result = returnedCoupon;
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpGet("GetAllCoupons")]
        public ResponseDto GetAllCoupons()
        {
            try
            {
                var coupons = _db.Coupons.ToList();
                if (coupons != null || coupons.Any())
                {
                    _responseDto.Result = _mapper.Map<List<CouponDto>>(coupons);
                }
                else
                {
                    _responseDto.Message = "No coupons found.";
                }
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpPost("CreateCoupon")]
        [Authorize(Roles = "admin")]
        public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                if (couponDto == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Invalid coupon data.";
                    return _responseDto;
                }
                couponDto.CreatedDate = DateTime.Now;

                var existingCoupon = _db.Coupons.FirstOrDefault(c => c.CouponCode == couponDto.CouponCode);
                if(existingCoupon != null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon code already exists.";
                    return _responseDto;
                }

                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
        [HttpPut("UpdateCoupon")]
        [Authorize(Roles = "admin")]
        public ResponseDto UpdateCoupon(CouponDto coupon)
        {
            try
            {
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Invalid coupon data.";
                    return _responseDto;
                }

                // Fetch existing coupon from DB
                Coupon existingCoupon = _db.Coupons.FirstOrDefault(c => c.CouponCode == coupon.CouponCode);
                if (existingCoupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon not found.";
                    return _responseDto;
                }

                // Map new values into the existing entity
                _mapper.Map(coupon, existingCoupon);

                // Save changes
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<CouponDto>(existingCoupon);
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }

        [HttpDelete("DeleteCoupon/{id}")]
        [Authorize(Roles = "admin")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(c => c.Id == id);
                if (coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon not found.";
                    return _responseDto;
                }
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
    }
}
