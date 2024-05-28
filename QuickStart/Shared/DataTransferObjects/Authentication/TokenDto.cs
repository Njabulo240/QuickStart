namespace Shared.DataTransferObjects.Authentication
{
    public record TokenDto(bool IsAuthSuccessful, string AccessToken, string RefreshToken);
}
