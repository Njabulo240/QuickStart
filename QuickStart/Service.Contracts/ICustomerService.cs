using Shared.DataTransferObjects.Customer;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(bool trackChanges);
        Task<CustomerDto> GetCustomerAsync(Guid customerId, bool trackChanges);
        Task<CustomerDto> CreateCustomerAsync(CustomerForCreationDto customer);
        Task DeleteCustomerAsync(Guid customerId, bool trackChanges);
        Task UpdateCustomerAsync(Guid customerId, CustomerForUpdateDto customerForUpdate, bool trackChanges);
    }
}
