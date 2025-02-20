using Microsoft.Extensions.Options;
using MormorsBageri.Data;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Interfaces;
using MormorsBageri.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});
// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//     options.JsonSerializerOptions.WriteIndented = true;
// });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}



using var scope = app.Services.CreateScope();

var Services = scope.ServiceProvider;

try
{
    var context = Services.GetRequiredService<DataContext>();

    await context.Database.MigrateAsync();
    await Seed.LoadProducts(context);
    await Seed.LoadSuppliers(context);
    await Seed.LoadSupplierProducts(context);
    await Seed.LoadAddressTypes(context);
    
    await Seed.LoadCustomers(context);
    await Seed.LoadOrders(context);
}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}
app.MapControllers();

app.Run();
