namespace Shared.DataTransferObjects.AuditLog
{
    public record YearAuditResultDto
    {
        public decimal TotalYearAudit { get; set; }
        public decimal IncreasePercentage { get; set; }
    }
}
