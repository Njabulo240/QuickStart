using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.UserRole
{
    public record UserRoleForUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}
