namespace Shared.DataTransferObjects.Account
{
    public record AccountDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string? AccountType { get; set; }
    }
}
