using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Orders -> OrderItems (One-to-Many)
            modelBuilder.Entity<Order>().HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);

            // Cart -> CartItems (One-to-Many)
            modelBuilder.Entity<Cart>().HasMany(c => c.CartItems)
               .WithOne(ci => ci.Cart)
               .HasForeignKey(ci => ci.CartId)
               .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Order>() .Property(x => x.TotalAmount) 
            //    .HasColumnType("decimal(18,2)"); 

            //modelBuilder.Entity<OrderItem>() .Property(x => x.UnitPrice) 
            //    .HasColumnType("decimal(18,2)"); }
        }
    }
}
