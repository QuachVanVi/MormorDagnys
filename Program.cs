using Microsoft.Extensions.Options;
using MormorsBageri.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

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
}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}
app.MapControllers();

app.Run();
