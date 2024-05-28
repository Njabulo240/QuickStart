namespace Shared.DataTransferObjects.AuditLog
{
    public record MonthAuditResultDto
    {
        public decimal TotalMonthAudit { get; set; }
        public decimal IncreasePercentage { get; set; }
    }
}
