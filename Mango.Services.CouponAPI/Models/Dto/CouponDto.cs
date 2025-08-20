namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        // Additional properties can be added as needed
    }
}
