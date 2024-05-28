namespace Shared.DataTransferObjects.AuditLog
{
    public record WeekAuditResultDto
    {
        public decimal TotalWeekAudit { get; set; }
        public decimal IncreasePercentage { get; set; }
    }
}
