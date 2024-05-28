namespace Shared.DataTransferObjects.AuditLog
{
    public record MonthTotalDto
    {
        public string? Month { get; set; }
        public List<DayTotalDto> DayTotals { get; set; } = new List<DayTotalDto>();
    }
}
