using Entities.Models;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(bool trackChanges);
        Task<Customer> GetCustomerAsync(Guid customerId, bool trackChanges);
        void CreateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
