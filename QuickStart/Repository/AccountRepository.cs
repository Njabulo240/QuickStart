using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateAccountForCustomer(Guid customerId, Account account)
        {
            account.CustomerId = customerId;
            Create(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }

        public async Task<Account> GetAccountAsync(Guid customerId, Guid id, bool trackChanges)
        {
            return await FindByCondition(e => e.CustomerId.Equals(customerId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(Guid customerId, bool trackChanges)
        {
            return await FindByCondition(e => e.CustomerId.Equals(customerId), trackChanges)
             .OrderBy(e => e.DateCreated).ToListAsync();
        }
    }
}
