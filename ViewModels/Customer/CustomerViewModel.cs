
using MormorsBageri.ViewModels.Address;

namespace MormorsBageri.ViewModels.Customer;

public class CustomerViewModel:CustomerBaseViewModel
{
    public int Id { get; set; }
    public IList<AddressViewModel> Addresses { get; set; }
}
