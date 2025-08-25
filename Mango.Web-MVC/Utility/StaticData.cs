namespace Mango.Web_MVC.Utility
{
    public class StaticData
    {
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
