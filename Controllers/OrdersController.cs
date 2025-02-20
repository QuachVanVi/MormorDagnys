
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Data;
using MormorsBageri.Entities;
using MormorsBageri.ViewModels.Order;

namespace MormorsBageri.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult>ListOrders(){
        var orders = await _context.Customers
        .Include(o => o.Orders)
        .Select(c => new{
           c.Id,
           c.StoreName,
           c.ContactPerson,
          Orders =  c.Orders
          .Select(c => new{
             c.Id,
            c.ProductId,
            c.Product.ProductName,
            c.Quantity,
            c.Price,
            c.Total
          })
        })
        .ToListAsync();

        return Ok(new{success = true, StatusCode=200,data = orders});
    }
    [HttpGet("{Id}")]
    public async Task<ActionResult>FindOrders(int id){
        var orders = await _context.Customers
        .Include(c => c.Orders)
        .Select(c => new{
           c.Id,
           c.StoreName,
           c.ContactPerson,
          Orders =  c.Orders
          .Select(c => new{
             c.Id,
            c.ProductId,
            c.Product.ProductName,
            c.Quantity,
            c.Price,
            Total = c.Price * c.Quantity
          })
        })
        .ToListAsync();

        return Ok(new{success = true, StatusCode=200,data = orders});
    }

    [HttpPost()]
    public async Task<ActionResult> AddOrder(OrderPostViewModel model){
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == model.CustomerId);

        if(customer == null )return NotFound($"Kunden Hittades ej.");

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.ProductId);

        if(product == null)return NotFound($"Produkten Hittades ej");

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Quantity = model.Quantity,
            Price = model.Price,
            ProductId = model.ProductId,
            CustomerId = model.CustomerId
        };
        try
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindOrders),new{id = order.Id}, order);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500,ex.Message);
        }
    }
}
