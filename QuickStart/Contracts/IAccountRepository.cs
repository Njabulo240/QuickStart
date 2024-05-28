using Entities.Models;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsAsync(Guid customerId, bool trackChanges);
        Task<Account> GetAccountAsync(Guid customerId, Guid id, bool trackChanges);
        void CreateAccountForCustomer(Guid customerId, Account account);
        void DeleteAccount(Account account);
    }
}
