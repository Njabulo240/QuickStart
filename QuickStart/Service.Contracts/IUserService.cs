using Shared.DataTransferObjects.User;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(string userId);
        Task UpdateUser(string userId, UserForUpdateDto updatedUser);
        Task<bool> DeleteUser(string userId);
    }
}
