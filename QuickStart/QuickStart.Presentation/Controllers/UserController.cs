using Microsoft.AspNetCore.Mvc;
using QuickStart.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.User;

namespace QuickStart.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.UserService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.UserService.GetUserById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDto updatedUser)
        {
            await _service.UserService.UpdateUser(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _service.UserService.DeleteUser(id);

            return NoContent();
        }

    }
}
