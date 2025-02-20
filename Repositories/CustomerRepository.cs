using eshop.api;
using Microsoft.EntityFrameworkCore;
using MormorsBageri.Data;
using MormorsBageri.Entities;
using MormorsBageri.Interfaces;
using MormorsBageri.ViewModels.Address;
using MormorsBageri.ViewModels.Customer;
using MormorsBageri.ViewModels.Order;

namespace MormorsBageri.Repositories;

public class CustomerRepository(DataContext context, IAddressRepository repo): ICustomerRepository
{
  private readonly DataContext _context = context;
  private readonly IAddressRepository _repo = repo;

  public async Task<bool> Add(CustomerPostViewModel model)
  {
    try
    {
      if (await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower().Trim()
        == model.Email.ToLower().Trim()) != null)
      {
        throw new Exception("Kunden finns redan");
      }

      var customer = new Customer
      {
        StoreName = model.StoreName,
        ContactPerson = model.ContactPerson,
        Email = model.Email,
        Phone = model.Phone
      };

      await _context.AddAsync(customer);

      foreach (var a in model.Addresses)
      {
        var address = await _repo.Add(a);

        await _context.CustomerAddresses.AddAsync(new CustomerAddress
        {
          Address = address,
          Customer = customer
        });
      }
      return await _context.SaveChangesAsync() > 0;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<CustomerViewModel> Find(int id)
  {

    try
    {
      var customer = await _context.Customers
        .Where(c => c.Id == id)
        .Include(c => c.CustomerAddresses)
          .ThenInclude(c => c.Address)
          .ThenInclude(c => c.PostalAddress)
        .Include(c => c.CustomerAddresses)
          .ThenInclude(c => c.Address)
          .ThenInclude(c => c.AddressType)
        .Include(c => c.Orders)
           
        .SingleOrDefaultAsync();

      if (customer is null)
      {
        throw new Exception($"Det finns ingen kund med id {id}");
      }

      var view = new CustomerViewModel
      {
        Id = customer.Id,
        StoreName = customer.StoreName,
        ContactPerson = customer.ContactPerson,
        Email = customer.Email,
        Phone = customer.Phone
      };

      var addresses = customer.CustomerAddresses.Select(c => new AddressViewModel
      {
        AddressLine = c.Address.AddressLine,
        PostalCode = c.Address.PostalAddress.PostalCode,
        City = c.Address.PostalAddress.City,
        AddressType = c.Address.AddressType.Value
      });

      view.Addresses = [.. addresses];
      return view;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  public async Task<IList<CustomersViewModel>> List()
  {
    var response = await _context.Customers
    .Include(c => c.Orders)
     .ThenInclude(c => c.Product)
    .ToListAsync();
    var customers = response.Select(c => new CustomersViewModel
    {
      Id = c.Id,
      StoreName = c.StoreName,
      ContactPerson = c.ContactPerson,
      Email = c.Email,
      Phone = c.Phone,
      Orders = c.Orders.Select(c => new OrderBaseViewModel{
        Id = c.Id,
        ProductId = c.ProductId,
        ProductName = c.Product.ProductName,
        CustomerId = c.CustomerId,
        OrderDate = c.OrderDate,
        Price = c.Price,
        Quantity = c.Quantity
      }).ToList()
      
    });

    return [.. customers];
  }

   
}
