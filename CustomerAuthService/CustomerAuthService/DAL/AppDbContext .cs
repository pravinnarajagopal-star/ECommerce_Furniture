using CustomerAuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuthService.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>()
        //        .HasMany(c => c.CustomerAddresses)
        //        .WithOne(a => a.Customer)
        //        .HasForeignKey(a => a.CustomerId)
        //        .HasPrincipalKey(c => c.CustomerId);
        //}
    }
}
