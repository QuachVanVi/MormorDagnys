using eshop.api;
using MormorsBageri.ViewModels.Customer;
using MormorsBageri.ViewModels.Order;

namespace MormorsBageri.Interfaces;

public interface ICustomerRepository
{

    public Task<IList<CustomersViewModel>> List();
    public Task<CustomerViewModel> Find(int id);
    public Task<bool> Add(CustomerPostViewModel model);
    public Task<object>GetCustomerOrder(int customerId);
    public Task<CustomerPatchViewModel>Update(int id, string contactPerson);

}
