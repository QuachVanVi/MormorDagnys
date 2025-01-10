

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
            ProductNumber = prod.ProductId,
            Product_Name = prod.ProductName,
            Product_Price = prod.Price,
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
        .Where(o => o.ProductId == id)
        .Select(products => new
        {
            products.ProductId,
            products.ItemNumber,
            products.ProductName,
            products.Price
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
        var product = new Product
        {
            ItemNumber = model.ItemNumber,
            ProductName = model.ProductName,
            Price = model.Price,
            Description = model.Description,
            Image = model.Image

        };
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindProducts), new{id = product.ProductId}, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    

    [HttpPut("{id}")]

    public async Task<ActionResult> UpdateProductPrice(int id, [FromQuery] double price)
    {
        var prod = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

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
    // public async Task<ActionResult> UpdateProductPrice(int id,  product)
    // {
    //     var productToUpdate = await _context.Products
    //     .Where(c => c.ProductId == id)
    //     .SingleOrDefaultAsync();

    //     if (productToUpdate is null) return BadRequest($"Det finns ingen produkt med produktnummer: {id}");

    //     foreach (var prod in product.SupplierProduct)
    //     {

    //         foreach (var item in productToUpdate.SupplierProducts)
    //         {
    //             item.ProductId = prod.ProductId;
    //             item.Price = prod.Price;
    //             item.ItemNumber = prod.ItemNumber;
    //         }

    //     }
    //     await _context.SaveChangesAsync();
    //     return NoContent();
    // }
}
