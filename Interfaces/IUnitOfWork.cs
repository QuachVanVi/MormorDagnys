namespace MormorsBageri.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository{get;}
      IAddressRepository AddressRepository { get; }


    Task<bool>Complete();
    bool HasChanges();
}
