namespace Contracts
{
    public interface IRepositoryManager
    {
        IAuditRepository Audit { get; }
        ICustomerRepository Customer { get; }
        IAccountRepository Account { get; }
        Task SaveAsync();
    }
}
