using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Authentication
{
    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }
        public ICollection<string>? Roles { get; init; } = new List<string>();
    }
}
