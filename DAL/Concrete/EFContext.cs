using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class EFContext:DbContext
    {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                               => optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=****;Password=***;Database=SampleDB;Pooling=true;");
            public DbSet<Categories> categories{get;set;}
            public DbSet<Products> products{get;set;}
            public DbSet<Suppliers> suppliers{get;set;}
            public DbSet<Users> users{get;set;}
    }
}