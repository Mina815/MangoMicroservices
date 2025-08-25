namespace Mango.Web_MVC.Models
{
    public class CouponDto
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int MinAmount { get; set; }
    }
}
