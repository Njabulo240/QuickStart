using Shared.DataTransferObjects.AuditLog;

namespace Service.Contracts
{
    public interface IAuditService
    {
        Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(bool trackChanges);
    }
}
