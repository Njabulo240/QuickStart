namespace Shared.DataTransferObjects.AuditLog
{
    public record DayTotalDto
    {
        public string? DayDate { get; set; }
        public decimal TotalAudit { get; set; }
    }
}
