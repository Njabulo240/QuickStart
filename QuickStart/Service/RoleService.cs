using AutoMapper;
using Contracts;
using Entities.Exceptions.Authentication;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObjects.UserRole;

namespace Service
{
    internal sealed class RoleService : IRoleService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly RoleManager<UserRole> _roleManager;
        public RoleService(ILoggerManager logger, IMapper mapper, RoleManager<UserRole> roleManager)
        {
            _logger = logger;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(UserRoleForCreationDto addRole)
        {
            var role = _mapper.Map<UserRole>(addRole);
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;

        }

        public async Task<bool> EditRole(string roleId, UserRoleForUpdateDto roleUpdate)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                throw new RoleNotFoundException(roleId);
            _mapper.Map(roleUpdate, role);
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;

        }

        public async Task<IEnumerable<UserRoleDto>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var rolesDto = _mapper.Map<IEnumerable<UserRoleDto>>(roles);

            return rolesDto;
        }

        public async Task<bool> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                throw new RoleNotFoundException(roleId);

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<UserRoleDto> GetRoleById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                throw new RoleNotFoundException(roleId);

            var roleDto = _mapper.Map<UserRoleDto>(role);
            return roleDto;
        }
    }
}
