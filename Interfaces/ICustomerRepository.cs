using eshop.api;
using MormorsBageri.ViewModels.Customer;

namespace MormorsBageri.Interfaces;

public interface ICustomerRepository
{

    public Task<IList<CustomersViewModel>> List();
    public Task<CustomerViewModel> Find(int id);
    public Task<bool> Add(CustomerPostViewModel model);

}
