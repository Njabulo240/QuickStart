using AutoMapper;
using Contracts;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.JwtFeatures;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IAccountService> _accountService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<IAuditService> _auditService;

        public ServiceManager(IRepositoryManager repositoryManager,
              ILoggerManager logger, IMapper mapper,
              UserManager<User> userManager, IConfiguration configuration,
              RoleManager<UserRole> roleManager, JwtHandler jwtHandler)
        {
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, logger, mapper));
            _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(
                logger, mapper, userManager, configuration, jwtHandler));

            _userService = new Lazy<IUserService>(() => new UserService(logger, mapper, userManager));
            _roleService = new Lazy<IRoleService>(() => new RoleService(logger, mapper, roleManager));
            _auditService = new Lazy<IAuditService>(() => new AuditService(repositoryManager, logger, mapper));
        }

        public ICustomerService CustomerService => _customerService.Value;
        public IAccountService AccountService => _accountService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IUserService UserService => _userService.Value;
        public IRoleService RoleService => _roleService.Value;
        public IAuditService AuditService => _auditService.Value;
    }
}
