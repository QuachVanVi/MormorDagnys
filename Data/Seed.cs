

using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Entities;

namespace MormorsBageri.Data;

public class Seed
{


     public static async Task LoadProducts(DataContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Products.Any()) return;
            var json = File.ReadAllText("Data/json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json,options);

            if(products is not null && products.Count > 0){
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
}

public static async Task LoadSupplierProducts(DataContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.SupplierProducts.Any()) return;
            var json = File.ReadAllText("Data/json/supplierProducts.json");
            var sp = JsonSerializer.Deserialize<List<SupplierProduct>>(json,options);

            if(sp is not null && sp.Count > 0){
                await context.SupplierProducts.AddRangeAsync(sp);
                await context.SaveChangesAsync();
            }
}

public static async Task LoadSuppliers(DataContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Suppliers.Any()) return;
            var json = File.ReadAllText("Data/json/supplier.json");
            var supplier = JsonSerializer.Deserialize<List<Supplier>>(json,options);

            if(supplier is not null && supplier.Count > 0){
                await context.Suppliers.AddRangeAsync(supplier);
                await context.SaveChangesAsync();
            }
}


}
