using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Authentication
{
    public record ForgotPasswordDto
    {

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? ClientURI { get; set; }

    }
}
