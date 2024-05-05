using Entities.Models;

namespace Contracts
{
    public interface IAuditRepository
    {
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync(bool trackChanges);
    }
}
