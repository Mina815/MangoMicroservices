using Mango.Service.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Service.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            //modelBuilder.Entity<Product>().HasData(new Product
            //{
            //    ProductId = 1,
            //    Name = "Mango",
            //    Price = 15,
            //    Description = "he5o he5o",
            //    ImageUrl = "https://placehold.co/603x403",
            //    CategoryName = "Fruit"
            //});
            //modelBuilder.Entity<Product>().HasData(new Product
            //{
            //    ProductId = 2,
            //    Name = "Apple",
            //    Price = 13.99,
            //    Description = "oh yeah",
            //    ImageUrl = "https://placehold.co/602x402",
            //    CategoryName = "Fruit"
            //});
        }
    }
}
