using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Coupon>().HasData(
                new Models.Coupon
                {
                    Id = 1,
                    CouponCode = "10OFF",
                    DiscountAmount = 10.0,
                    CreatedDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddMonths(1),
                    IsActive = true,
                    MinAmount = 20
                },
                new Models.Coupon
                {
                    Id = 2,
                    CouponCode = "20OFF",
                    DiscountAmount = 20.0,
                    CreatedDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddMonths(1),
                    IsActive = true,
                    MinAmount = 40
                }
            );
        }

    }
}
