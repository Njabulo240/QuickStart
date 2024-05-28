using Shared.DataTransferObjects.Account;

namespace Service.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAccountsAync(Guid customerId, bool trackChanges);
        Task<AccountDto> GetAccountAsync(Guid customerId, Guid id, bool trackChanges);
        Task<AccountDto> CreateAccountForCustomerAsync(Guid customerId, AccountForCreationDto accountForCreation, bool trackChanges);
        Task DeleteAccountForCustomerAsync(Guid customerId, Guid id, bool trackChanges);
        Task UpdateAccountForCustomerAsync(Guid customerId, Guid id, AccountForUpdateDto accountForUpdate, bool cusTrackChanges, bool accTrackChanges);

    }
}
