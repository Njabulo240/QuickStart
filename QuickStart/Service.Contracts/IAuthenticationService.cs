using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.Authentication;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> ValidateUser(UserForAuthenticationDto userForAuthentication);
        Task<IdentityResult> ChangePassword(ChangePasswordDto changePassword);
        Task<IdentityResult> ChangeDefaultPassword(string userName, ChangeDefaultPasswordDto changePassword);
        Task<IdentityResult> ForgetPassword(ForgotPasswordDto forgotPassword);
        Task<IdentityResult> ResetPassword(ResetPasswordDto resetPassword);

    }
}
