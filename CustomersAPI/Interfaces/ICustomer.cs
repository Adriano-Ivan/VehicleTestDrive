using CustomersAPI.Models;

namespace CustomersAPI.Interfaces
{
    public interface ICustomer
    {
        Task AddCustomer(Customer customer);
    }
}
