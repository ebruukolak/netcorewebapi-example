using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class EFContext:DbContext
    {
        
             protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=SampleDB;Username=****;Password=****");
            public DbSet<Category> Categories{get;set;}
            public DbSet<Product> Products{get;set;}
            public DbSet<Supplier> Suppliers{get;set;}
    }
}