

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Data;
using MormorsBageri.Entities;
using MormorsBageri.ViewModels;

namespace MormorsBageri.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SupplierProductsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListSupplierProducts()
    {
        var supplierProduct = await _context.Suppliers
        .Include(o => o.SupplierProducts)
        .Select(c => new
        {
            SupplierNumber = c.SupplierId,
            Supplier = c.SupplierName,
            SupplierProducts = c.SupplierProducts
            .Select(s => new
            {
                s.Product.ProductName,
                s.ProductId,
                s.ItemNumber,
                s.Price

            })
        })
        .ToListAsync();
        return Ok(new { success = true, statusCode = 200, data = supplierProduct });
    }

    [HttpGet("{id}")]

    public async Task<ActionResult> FindSupplierProduct(int id)
    {
        var supplierProduct = await _context.Suppliers
        .Include(c => c.SupplierProducts)
        .Select(s => new
        {
            SupplierNumber = s.SupplierId,
            Supplier = s.SupplierName,
            SupplierProduct = s.SupplierProducts
            .Select(supp => new
            {
                supp.Product.ProductName,
                supp.ProductId,
                supp.ItemNumber,
                supp.Price
            })
        })
        .SingleOrDefaultAsync(sp => sp.SupplierNumber == id);

        if (supplierProduct is null)
            return NotFound(new { success = false, message = $"Tyvärr kunde vi inte hitta leverans produkten med id: {id}" });
        else
            return Ok(new { success = true,StatusCode=200, supplierProduct });
    }

    [HttpPost()]
    public async Task<ActionResult> AddSupplierProduct(SupplierProductPostViewModel model)
    {
        var sp = await _context.SupplierProducts.FirstOrDefaultAsync(s => s.SupplierId == model.SupplierId && s.ProductId == model.ProductId);

        if (sp != null)
        {
            return BadRequest(new { success = false, message = $"Denna produkten för leverantören existerar redan {0}, {1}", model.ProductId, model.SupplierId });
        }

        var supplier = new SupplierProduct
        {
            SupplierId = model.SupplierId,
            ProductId = model.ProductId,
            ItemNumber = model.ItemNumber,
            Price = model.Price,
            Stock = model.Stock
        };
        try
        {
            await _context.SupplierProducts.AddAsync(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindSupplierProduct), new { id = supplier.SupplierId }, supplier);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSupplierPrice(int id,[FromQuery] SupplierProductPatchViewModel model )

    {
        var sp = await _context.SupplierProducts.FirstOrDefaultAsync(s => s.ProductId == id && s.SupplierId == model.SupplierId);

        if(sp == null)
        {
            return NotFound(new{success = false, message = $"Produkten som du försöker uppdatera existerar inte längre {0}", id});

        }
        sp.Price = model.Price;
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



