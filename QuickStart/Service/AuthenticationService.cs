using AutoMapper;
using Contracts;
using EmailService;
using Entities.Exceptions.Authentication;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.JwtFeatures;
using Shared.DataTransferObjects.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;

        private User? _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration, JwtHandler jwtHandler,
            IEmailSender emailSender)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtHandler = jwtHandler;
            _emailSender = emailSender;

        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var defaultPassword = "Password.321";
            var result = await _userManager.CreateAsync(user, defaultPassword);

            var response = new RegistrationResponseDto
            {
                IsSuccessfulRegistration = result.Succeeded,
                Errors = result.Succeeded ? null : result.Errors.Select(e => e.Description).ToList()
            };

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

                var message = new Message(new string[] { user.Email }, "Default Password", "UserName: " + "'" + user.UserName + "'" + ", Password: " + "'" + defaultPassword + "'", null);
                await _emailSender.SendEmailAsync(message);
            }


            return response;
        }



        public async Task<AuthResponseDto> ValidateUser(UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.UserName);

            if (user == null)
                return new AuthResponseDto { ErrorMessage = "Invalid Request" };

            await _userManager.AccessFailedAsync(user);

            if (await _userManager.IsLockedOutAsync(user))
            {
                var content = $"Your account is locked out. To reset the password click this link: {userForAuthentication.ClientURI}";
                var message = new Message(new string[] { user.Email }, "Locked out account information", content, null);

                await _emailSender.SendEmailAsync(message);

                return new AuthResponseDto { ErrorMessage = "The account is locked out" };
            }

            if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {

                return new AuthResponseDto { ErrorMessage = "Invalid Authentication" };
            }


            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            await _userManager.ResetAccessFailedCountAsync(user);

            return new AuthResponseDto { IsAuthSuccessful = true, Token = token };
        }



        public async Task<IdentityResult> ChangePassword(ChangePasswordDto changePassword)
        {

            var user = await _userManager.FindByEmailAsync(changePassword.Email);
            if (user == null)
                throw new EmailBadRequest();

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, changePassword.PreviousPassword);

            if (!isPasswordValid)
                throw new PasswordBadRequest();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, changePassword.Password);

            return resetPassResult;
        }


        public async Task<IdentityResult> ChangeDefaultPassword(string userName, ChangeDefaultPasswordDto changePassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new EmailBadRequest();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, changePassword.Password);

            return resetPassResult;
        }

        public async Task<IdentityResult> ForgetPassword(ForgotPasswordDto forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
                throw new EmailBadRequest();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
               {
                  {"token", token },
                  {"email", forgotPassword.Email }
               };

            var callback = QueryHelpers.AddQueryString(forgotPassword.ClientURI, param);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                throw new EmailBadRequest();

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            await _userManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));
            return resetPassResult;
        }
    }
}
