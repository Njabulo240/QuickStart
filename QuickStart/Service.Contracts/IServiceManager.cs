namespace Service.Contracts
{
    public interface IServiceManager
    {
        IAuditService AuditService { get; }
        IAuthenticationService AuthenticationService { get; }
        ICustomerService CustomerService { get; }
        IAccountService AccountService { get; }
        IUserService UserService { get; }
        IRoleService RoleService { get; }
    }
}
