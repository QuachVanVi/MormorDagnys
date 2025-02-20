using MormorsBageri.ViewModels.Address;
using MormorsBageri.ViewModels.Customer;

namespace eshop.api;

public class CustomerPostViewModel : CustomerBaseViewModel
{
  public IList<AddressPostViewModel> Addresses { get; set; }
}
