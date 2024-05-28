namespace Shared.DataTransferObjects.Account
{
    public abstract record AccountForManipulationDto
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? AccountType { get; set; }
    }
}
