namespace Contracts
{
    public interface IRepositoryManager
    {
        IAuditRepository Audit { get; }
        Task SaveAsync();
    }
}
