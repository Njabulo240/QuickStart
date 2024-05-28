namespace Shared.DataTransferObjects.Authentication
{
    public record EmailConfirmationRequestDto
    {
        public string? Email { get; set; }
        public string? ClientURI { get; set; }
    }
}
