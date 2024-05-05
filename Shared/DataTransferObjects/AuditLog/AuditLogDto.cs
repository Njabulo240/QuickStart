namespace Shared.DataTransferObjects.AuditLog
{
    public class AuditLogDto
    {
        public required string EntityName { get; set; }
        public required string Action { get; set; }
        public required DateTime Timestamp { get; set; }
        public required string Changes { get; set; }
    }
}
