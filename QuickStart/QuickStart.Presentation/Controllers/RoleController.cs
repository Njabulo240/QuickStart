using Microsoft.AspNetCore.Mvc;
using QuickStart.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.UserRole;

namespace QuickStart.Presentation.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RoleController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            //  throw new Exception("Exception");
            var roles = await _service.RoleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRole([FromBody] UserRoleForCreationDto addRole)
        {
            await _service.RoleService.CreateRole(addRole);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] UserRoleForUpdateDto roleUpdate)
        {
            await _service.RoleService.EditRole(id, roleUpdate);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _service.RoleService.DeleteRole(id);

            return NoContent();
        }

        [HttpGet("{id}", Name = "RoleById")]
        public async Task<IActionResult> GetRole(string id)
        {
            var user = await _service.RoleService.GetRoleById(id);
            return Ok(user);
        }
    }
}
