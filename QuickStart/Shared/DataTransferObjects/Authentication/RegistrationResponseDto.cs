namespace Shared.DataTransferObjects.Authentication
{
    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
