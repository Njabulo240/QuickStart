using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.AuditLog;
using System.Globalization;

namespace Service
{
    internal sealed class AuditService : IAuditService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AuditService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(bool trackChanges)
        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);

            var auditsDto = _mapper.Map<IEnumerable<AuditLogDto>>(audits);

            return auditsDto;
        }

        public async Task<List<MonthDto>> GetMonthAsync(bool trackChanges)

        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);

            var groupedByMonth = audits
                .GroupBy(o => new { o.Timestamp.Year, o.Timestamp.Month })
                .Select(group => new MonthDto
                {
                    Month = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month)} {group.Key.Year}",
                })
                 .OrderBy(m => m.Month).ToList();

            return groupedByMonth;
        }

        public async Task<MonthTotalDto> GetTotalAuditForSingleMonthAsync(string monthYear, bool trackChanges)
        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);
            var selectedMonth = audits
                .GroupBy(o => new { o.Timestamp.Year, o.Timestamp.Month })
                .FirstOrDefault(group => $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month)} {group.Key.Year}" == monthYear);

            if (selectedMonth == null)
            {
                return null;
            }

            var monthTotalDto = new MonthTotalDto
            {
                Month = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(selectedMonth.Key.Month)} {selectedMonth.Key.Year}",
                DayTotals = selectedMonth
                .GroupBy(o => o.Timestamp.ToString("dd/MM"))
                .Select(dayGroup => new DayTotalDto
                {
                    DayDate = dayGroup.Key,
                    TotalAudit = dayGroup.Count()
                })
              .ToList()
            };

            return monthTotalDto;
        }

    }
}
