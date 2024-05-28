namespace Shared.DataTransferObjects.Customer
{
    public record CustomerDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? accountCount { get; set; }
        public Boolean status { get; set; }
    }
}
