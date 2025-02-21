
using eshop.api;
using Microsoft.AspNetCore.Mvc;
using MormorsBageri.Entities;
using MormorsBageri.Interfaces;
using MormorsBageri.ViewModels.Order;

namespace MormorsBageri.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(IUnitOfWork unitOfWork) : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  [HttpGet()]
  public async Task<ActionResult> GetAllCustomers()
  {
    var customers = await _unitOfWork.CustomerRepository.List();
    return Ok(new { success = true, data = customers });
  }

  [HttpGet("{id}/orders")]
  public async Task<ActionResult>GeCustomerOrder(int id)
  {
    try
    {
      return Ok(new { success = true, data = await _unitOfWork.CustomerRepository.GetCustomerOrder(id) });
    }
    catch (Exception ex)
    {
      return NotFound(new { success = false, message = ex.Message });
    }
  
  }



  [HttpGet("{id}")]
  public async Task<ActionResult> GetCustomer(int id)
  {
    try
    {
      return Ok(new { success = true, data = await _unitOfWork.CustomerRepository.Find(id) });
    }
    catch (Exception ex)
    {
      return NotFound(new { success = false, message = ex.Message });
    }
  }

  [HttpPost()]
  public async Task<ActionResult> AddCustomer(CustomerPostViewModel model)
  {
    try
    {
      var result = await _unitOfWork.CustomerRepository.Add(model);
      if (result)
      {
        return StatusCode(201);
      }
      else
      {
        return BadRequest();
      }
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }

  }
  
 
       
}
