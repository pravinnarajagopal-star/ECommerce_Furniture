using Microsoft.EntityFrameworkCore;
using ProductService.Models;


namespace ProductService.DAL;


public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
