

using Microsoft.EntityFrameworkCore;
using MormorsBageri.Entities;

namespace MormorsBageri.Data;

    public class DataContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        

public DataContext(DbContextOptions options) : base(options){
            
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>().HasKey(o=> new{o.ProductId, o.SupplierId});
    }
}
