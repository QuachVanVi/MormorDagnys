

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Data;
using MormorsBageri.Entities;
using MormorsBageri.ViewModels;

namespace MormorsBageri.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProductsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var product = await _context.Products
        .Include(c => c.SupplierProducts)
        .Select(prod => new
        {
            ProductNumber = prod.Id,
            Product_Name = prod.ProductName,
            Product_Price = prod.Price,
            prod.QuantityPerPackage,
            prod.Weight,
            prod.BestBeforeDate,
            prod.ProductionDate,
            SupplierProducts = prod.SupplierProducts

            .Select(products => new
            {
                products.Supplier.SupplierName,
                products.ItemNumber,
                products.Product.ProductName,
                products.Price
            })

        })

        .ToListAsync();

        return Ok(new { success = true, statusCode = 200, data = product });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProducts(int id)
    {
        var product = await _context.Products
        .Where(o => o.Id == id)
        .Select(products => new
        {
            products.Id,
            products.ItemNumber,
            products.ProductName,
            products.Price,
            products.ProductionDate,
            products.BestBeforeDate,
            products.QuantityPerPackage,
            products.Weight
        })
        .SingleOrDefaultAsync();

        if (product is null)
        {
            return NotFound(new { success = false, statusCode = 404, message = $"Tyvärr vi kunde inte hitta produktnummer: {id}" });
        }
        return Ok(new { success = true, statusCode = 200, data = product });

    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(ProductPostViewModel model)
    {
        var prod = await _context.Products.FirstOrDefaultAsync(p => p.ItemNumber == model.ItemNumber);

        
        if (prod != null)
        {
            return BadRequest(new { success = false, message = $"Produkten existerar redan {0}", model.ProductName });

        }

        await _context.SaveChangesAsync();

        var product = new Product
        {
            
            ItemNumber = model.ItemNumber,
            ProductName = model.ProductName,    
            Price = model.Price,
            Weight = model.Weight,
            QuantityPerPackage = model.QuantityPerPackage,
            ProductionDate = DateTime.Now,
            BestBeforeDate = DateTime.Now.AddDays(30)
            

        };
        
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindProducts), new{id = product.Id}, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    

    [HttpPut("{id}")]

    public async Task<ActionResult> UpdateProductPrice(int id, [FromQuery] double price)
    {
        var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if(prod == null)
        {
            return NotFound(new { success = false, message = $"Produkten som du försöker uppdatera existerar inte längre ", id});

        }
        prod.Price = price;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
        return NoContent();
    }
}
