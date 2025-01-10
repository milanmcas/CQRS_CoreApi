using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Data
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=milan;Trust Server Certificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, Name = "Apple iPad", Price = 1000 },
            new Product() { Id = 2, Name = "Samsung Smart TV", Price = 1500 },
            new Product() { Id = 3, Name = "Nokia 130", Price = 1200 },
            new Product() { Id = 4, Name = "Nokia 450", Price = 12000 });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
    }
}
