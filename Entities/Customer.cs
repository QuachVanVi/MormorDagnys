
using System.ComponentModel.DataAnnotations;

namespace MormorsBageri.Entities;


public class Customer
{
  public int Id { get; set; }
  public string StoreName { get; set; }
  public string ContactPerson { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }

  public IList<CustomerAddress> CustomerAddresses { get; set; }

  
  public IList<Order> Orders { get; set; }
}
