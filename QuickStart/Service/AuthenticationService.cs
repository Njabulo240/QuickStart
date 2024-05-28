using AutoMapper;
using Contracts;
using Entities.Exceptions.Authentication;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
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

        private User? _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration, JwtHandler jwtHandler)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtHandler = jwtHandler;

        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var defaultPassword = "Password.321";
            var result = await _userManager.CreateAsync(user, defaultPassword);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return result;
        }



        public async Task<AuthResponseDto> ValidateUser(UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.UserName);

            if (user == null)
                return new AuthResponseDto { ErrorMessage = "Invalid Authentication hhhhh" };

            if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                return new AuthResponseDto { ErrorMessage = "Invalid Authentication" };
            }


            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

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
    }
}
