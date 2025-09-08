using Mango.Web_MVC.Models;

namespace Mango.Web_MVC.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByCodeAsync(string CouponCode);
        Task<ResponseDto?> CreateCouponAsync(CouponDto coupon);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon);
        Task<ResponseDto?> DeleteCouponAsync(int couponId);
        Task<ResponseDto?> GetCouponById(int CouponId);
    }
}
