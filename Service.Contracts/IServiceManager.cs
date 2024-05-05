namespace Service.Contracts
{
    public interface IServiceManager
    {
        IAuditService AuditService { get; }
    }
}
