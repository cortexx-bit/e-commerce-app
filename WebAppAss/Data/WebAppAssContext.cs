using Microsoft.EntityFrameworkCore;
using WebAppAss.Models;

namespace WebAppAss.Data
{
    public class WebAppAssContext : DbContext
    {
        public WebAppAssContext(DbContextOptions<WebAppAssContext> options)
            : base(options)
        {
        }

        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Drink> Drinks { get; set; }
    }
}