using Microsoft.Extensions.Options;
using MormorsBageri.Data;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Interfaces;
using MormorsBageri.Repositories;

var builder = WebApplication.CreateBuilder(args);

// var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
builder.Services.AddDbContext<DataContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
//    options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);
});
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
