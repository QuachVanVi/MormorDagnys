using MormorsBageri.Entities;
using MormorsBageri.ViewModels.Address;

namespace MormorsBageri.Interfaces;

public interface IAddressRepository
{
  public Task<Address> Add(AddressPostViewModel model);
}
