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

        public async Task<MonthAuditResultDto> GetTotalLatestMonthAuditAsync(bool trackChanges)
        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            var lastMonthTotal = audits
                .Where(o => o.Timestamp.Month == currentMonth - 1 && o.Timestamp.Year == currentYear).Count();
            var currentMonthTotal = audits
                .Where(o => o.Timestamp.Month == currentMonth && o.Timestamp.Year == currentYear).Count();

            var increasePercentage = CalculateIncreasePercentage(lastMonthTotal, currentMonthTotal);

            var result = new MonthAuditResultDto
            {
                TotalMonthAudit = currentMonthTotal,
                IncreasePercentage = increasePercentage
            };

            return result;
        }

        public async Task<WeekAuditResultDto> GetTotalLatestWeekAuditAsync(bool trackChanges)
        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);
            var currentDate = DateTimeOffset.Now;
            var currentWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(currentDate.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            var currentYear = currentDate.Year;

            var lastWeekTotalAudit = audits
             .Where(o => o.Timestamp.Year == currentYear - 1).Count();

            var currentWeekTotalAudit = audits
                .Where(o => o.Timestamp.Year == currentYear).Count();

            var increasePercentage = CalculateIncreasePercentage(lastWeekTotalAudit, currentWeekTotalAudit);

            var result = new WeekAuditResultDto
            {
                TotalWeekAudit = currentWeekTotalAudit,
                IncreasePercentage = increasePercentage
            };

            return result;
        }

        public async Task<YearAuditResultDto> GetTotalLatestYearAuditAsync(bool trackChanges)
        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);
            var currentYear = DateTime.Now.Year;
            var latestYearTotal = audits
                .Where(o => o.Timestamp.Year == currentYear - 1).Count();

            var currentYearTotal = audits
                .Where(o => o.Timestamp.Year == currentYear).Count();

            var increasePercentage = CalculateIncreasePercentage(latestYearTotal, currentYearTotal);

            var result = new YearAuditResultDto
            {
                TotalYearAudit = currentYearTotal,
                IncreasePercentage = increasePercentage
            };

            return result;
        }

        private decimal CalculateIncreasePercentage(decimal oldValue, decimal newValue)
        {
            if (oldValue == 0)
            {
                return newValue == 0 ? 0 : 100;
            }

            return ((newValue - oldValue) / oldValue) * 100;
        }
    }
}
