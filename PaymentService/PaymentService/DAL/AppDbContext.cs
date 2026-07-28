using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Payment> Payments { get; set; }
    }
}
