using Shared.DataTransferObjects.AuditLog;

namespace Service.Contracts
{
    public interface IAuditService
    {
        Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(bool trackChanges);

        // analysis
        Task<List<MonthDto>> GetMonthAsync(bool trackChanges);
        Task<MonthTotalDto> GetTotalAuditForSingleMonthAsync(string monthYear, bool trackChanges);
    }
}
