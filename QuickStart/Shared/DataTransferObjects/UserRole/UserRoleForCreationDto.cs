using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.UserRole
{
    public record UserRoleForCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
