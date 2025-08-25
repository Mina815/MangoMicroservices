using Mango.Web_MVC.Models;
using Mango.Web_MVC.Services.IServices;
using Mango.Web_MVC.Utility;

namespace Mango.Web_MVC.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = coupon,
                Url = StaticData.CouponAPIBase + "/api/coupon/CreateCoupon",
                AccessToken = ""
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.CouponAPIBase + "/api/coupon/DeleteCoupon/" + couponId,
                AccessToken = ""
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponAPIBase + "/api/coupon/GetAllCoupons",
                AccessToken = ""
            });
        }

        public async Task<ResponseDto?> GetCouponByCodeAsync(string CouponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponAPIBase + "/api/coupon/GetCouponByCode/" + CouponCode,
                AccessToken = ""
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = coupon,
                Url = StaticData.CouponAPIBase + "/api/coupon/UpdateCoupon",
                AccessToken = ""
            });
        }
    }
}
