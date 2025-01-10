using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Data;
using MormorsBageri.Entities;
using MormorsBageri.ViewModels;

namespace MormorsBageri.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SupplierController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        var suppliers = await _context.Suppliers

        .Select(s => new
        {
            s.SupplierId,
            s.SupplierName,
            s.SupplierEmail,
            s.SupplierPhone,
            s.ContactPerson
        })

        .ToListAsync();
        return Ok(new { success = true, StatusCode = 200, data = suppliers });
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        var suppliers = await _context.Suppliers
        .Select(supplier => new
        {
            SupplierNumber = supplier.SupplierId,
            Supplier = new
            {
                supplier.SupplierName,
                supplier.SupplierPhone,
                supplier.SupplierEmail,
                supplier.ContactPerson
            }
        })
        .SingleOrDefaultAsync(s => s.SupplierNumber == id);

        if (suppliers != null)
            return Ok(new { success = true, suppliers });
        else
            return NotFound(new { success = false, message = $"Tyvärr kunde vi inte hitta leverantören med id: {id}" });
    }

    [HttpPost()]
    public async Task<ActionResult> AddSupplier(SupplierPostViewModel model)
    {
        var sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierName == model.SupplierName);

        if (sup != null)
        {
            return BadRequest(new { success = false, message = $"Produkten existerar redan {0}", model.SupplierName });
        }

        var supplier = new Supplier
        {
            SupplierName = model.SupplierName,
            SupplierPhone = model.SupplierPhone,
            SupplierEmail = model.SupplierEmail,
            ContactPerson = model.ContactPerson
        };
        try
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindSupplier), new {id = supplier.SupplierId}, supplier);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }



}
