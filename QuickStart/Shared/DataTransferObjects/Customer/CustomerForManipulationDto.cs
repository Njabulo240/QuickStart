namespace Shared.DataTransferObjects.Customer
{
    public abstract record CustomerForManipulationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
