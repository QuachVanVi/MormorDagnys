

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
public static async Task LoadCustomers(DataContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Customers.Any()) return;
            var json = File.ReadAllText("Data/json/customers.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(json,options);

            if(customers is not null && customers.Count > 0){
                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }
}

public static async Task LoadAddressTypes(DataContext context)
  {
    var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
    if (context.AddressTypes.Any()) return;

    var json = await File.ReadAllTextAsync("Data/json/addressTypes.json");
    var types = JsonSerializer.Deserialize<List<AddressType>>(json, options);

    if (types is not null && types.Count > 0)
    {
      await context.AddressTypes.AddRangeAsync(types);
      await context.SaveChangesAsync();
    }
  }
  public static async Task LoadOrders(DataContext context)
  {
    var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
    if (context.Orders.Any()) return;

    var json = await File.ReadAllTextAsync("Data/json/orders.json");
    var order = JsonSerializer.Deserialize<List<Order>>(json, options);

    if (order is not null && order.Count > 0)
    {
      await context.Orders.AddRangeAsync(order);
      await context.SaveChangesAsync();
    }
  }
}
