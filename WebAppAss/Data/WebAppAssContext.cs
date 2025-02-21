using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAppAss.Models;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAss.Data
{
    public class WebAppAssContext : IdentityDbContext
    {
        public WebAppAssContext(DbContextOptions<WebAppAssContext> options)
            : base(options)
        {
        }

        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        [NotMapped]
        public DbSet<CheckoutItem> CheckoutItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderHistory>()
                .HasKey(oh => oh.OrderNo);
            modelBuilder.Entity<OrderHistory>()
                .Property(oh => oh.OrderNo)
                .ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<BasketItem>()
                .HasKey(bi => new { bi.BasketID, bi.ItemID, bi.ItemType });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderNo, oi.ItemID, oi.ItemType });
        }
    }
}