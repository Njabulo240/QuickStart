using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Authentication
{
    public record ChangeDefaultPasswordDto
    {
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
