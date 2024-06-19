using Microsoft.AspNetCore.Mvc;
using QuickStart.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.Authentication;

namespace QuickStart.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var response = await _service.AuthenticationService.RegisterUser(userForRegistration);

            if (!response.IsSuccessfulRegistration)
            {
                return BadRequest(new { IsSuccessfulRegistration = false, Errors = response.Errors });
            }

            return StatusCode(201, new { IsSuccessfulRegistration = true });
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var authResponse = await _service.AuthenticationService.ValidateUser(user);

            if (authResponse.IsAuthSuccessful)
                return Ok(authResponse);
            else
                return Unauthorized(authResponse);
        }


        [HttpPost("ChangePassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto password)
        {
            var result = await _service.AuthenticationService.ChangePassword(password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("{user}/ChangeDefulatPassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ChangeDefaultPassword(string user, [FromBody] ChangeDefaultPasswordDto password)
        {
            var result = await _service.AuthenticationService.ChangeDefaultPassword(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("ForgotPassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> forgetPassword([FromBody] ForgotPasswordDto forgotPassword)
        {

            var result = await _service.AuthenticationService.ForgetPassword(forgotPassword);

            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            var result = await _service.AuthenticationService.ResetPassword(resetPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return StatusCode(201);
        }
    }
}
