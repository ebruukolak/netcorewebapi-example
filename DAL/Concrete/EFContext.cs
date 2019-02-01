using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class EFContext:DbContext
    {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                               => optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=**;Password=**;Database=**;Pooling=true;");
            public DbSet<Categories> CategoryContext{get;set;}
            public DbSet<Products> products{get;set;}
            public DbSet<Suppliers> SupplierContext{get;set;}
    }
}