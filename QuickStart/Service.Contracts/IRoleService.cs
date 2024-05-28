using Shared.DataTransferObjects.UserRole;

namespace Service.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAllRoles();
        Task<bool> CreateRole(UserRoleForCreationDto addRole);
        Task<bool> EditRole(string roleId, UserRoleForUpdateDto roleUpdate);
        Task<bool> DeleteRole(string roleId);
        Task<UserRoleDto> GetRoleById(string roleId);

    }
}
