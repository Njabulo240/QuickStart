using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(customerId), trackChanges)
                 .Include(c => c.Accounts)
                .SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.FirstName)
                .Include(c => c.Accounts)
                .ToListAsync();
        }
    }
}
