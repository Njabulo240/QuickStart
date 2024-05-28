using AutoMapper;
using Contracts;
using Entities.Exceptions.Account;
using Entities.Exceptions.Customer;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Account;

namespace Service
{
    internal sealed class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public AccountService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountForCustomerAsync(Guid customerId, AccountForCreationDto accountForCreation, bool trackChanges)
        {
            await CheckIfCustomerExists(customerId, trackChanges);

            var accountEntity = _mapper.Map<Account>(accountForCreation);

            _repository.Account.CreateAccountForCustomer(customerId, accountEntity);
            await _repository.SaveAsync();

            var accountToReturn = _mapper.Map<AccountDto>(accountForCreation);

            return accountToReturn;
        }

        public async Task DeleteAccountForCustomerAsync(Guid customerId, Guid id, bool trackChanges)
        {
            await CheckIfCustomerExists(customerId, trackChanges);

            var accountDb = await GetAccountForCustomerAndCheckIfItExists(customerId, id, trackChanges);

            _repository.Account.DeleteAccount(accountDb);
            await _repository.SaveAsync();
        }

        public async Task<AccountDto> GetAccountAsync(Guid customerId, Guid id, bool trackChanges)
        {
            await CheckIfCustomerExists(customerId, trackChanges);

            var accountDb = await GetAccountForCustomerAndCheckIfItExists(customerId, id, trackChanges);

            var account = _mapper.Map<AccountDto>(accountDb);
            return account;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAync(Guid customerId, bool trackChanges)
        {
            await CheckIfCustomerExists(customerId, trackChanges);

            var accountFromDb = await _repository.Account.GetAccountsAsync(customerId, trackChanges);
            var accountDto = _mapper.Map<IEnumerable<AccountDto>>(accountFromDb);

            return accountDto;
        }

        public async Task UpdateAccountForCustomerAsync(Guid customerId, Guid id, AccountForUpdateDto accountForUpdate, bool cusTrackChanges, bool accTrackChanges)
        {
            await CheckIfCustomerExists(customerId, cusTrackChanges);

            var accountDb = await GetAccountForCustomerAndCheckIfItExists(customerId, id, accTrackChanges);

            _mapper.Map(accountForUpdate, accountDb);
            await _repository.SaveAsync();
        }

        private async Task CheckIfCustomerExists(Guid customerId, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
        }

        private async Task<Account> GetAccountForCustomerAndCheckIfItExists
            (Guid customerId, Guid id, bool trackChanges)
        {
            var accountDb = await _repository.Account.GetAccountAsync(customerId, id, trackChanges);
            if (accountDb is null)
                throw new AccountNotFoundException(id);

            return accountDb;
        }
    }
}
