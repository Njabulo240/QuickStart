namespace Shared.DataTransferObjects.UserRole
{
    public record UserRoleDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
