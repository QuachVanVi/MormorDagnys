using MormorsBageri.ViewModels.Order;

namespace MormorsBageri.ViewModels.Customer;

public class CustomerBaseViewModel
{
  public string StoreName { get; set; }
  public string ContactPerson { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
    public IList<OrderBaseViewModel> Orders { get; set; }

}