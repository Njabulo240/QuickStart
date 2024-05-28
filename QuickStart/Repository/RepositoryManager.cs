using Contracts;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<IAuditRepository> _auditRepository;

        public RepositoryManager(RepositoryContext repositoryContext, RoleManager<UserRole> roleManager)
        {
            _repositoryContext = repositoryContext;
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(repositoryContext));
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(repositoryContext));
            _auditRepository = new Lazy<IAuditRepository>(() => new AuditRepository(repositoryContext));


        }

        public ICustomerRepository Customer => _customerRepository.Value;
        public IAccountRepository Account => _accountRepository.Value;
        public IAuditRepository Audit => _auditRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
