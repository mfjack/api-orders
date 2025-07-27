using api_orders.Models;
using Microsoft.EntityFrameworkCore;

namespace api_orders.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Order> HUB { get; set; }
    }
}