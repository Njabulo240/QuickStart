using Shared.DataTransferObjects.AuditLog;

namespace Service.Contracts
{
    public interface IAuditService
    {
        Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(bool trackChanges);

        // analysis
        Task<List<MonthDto>> GetMonthAsync(bool trackChanges);
        Task<MonthTotalDto> GetTotalAuditForSingleMonthAsync(string monthYear, bool trackChanges);
        Task<YearAuditResultDto> GetTotalLatestYearAuditAsync(bool trackChanges);
        Task<MonthAuditResultDto> GetTotalLatestMonthAuditAsync(bool trackChanges);
        Task<WeekAuditResultDto> GetTotalLatestWeekAuditAsync(bool trackChanges);
    }
}
