using AutoMapper;
using Contracts;
using Entities.Exceptions.Authentication;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObjects.User;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var userDto in userDtos)
            {
                var user = await _userManager.FindByIdAsync(userDto.Id);
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles;
            }

            return userDtos;
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new UserNotFoundException(userId);

            var userDto = _mapper.Map<UserDto>(user);

            var roles = await _userManager.GetRolesAsync(user);

            userDto.Roles = roles;

            return userDto;
        }


        public async Task UpdateUser(string userId, UserForUpdateDto updatedUser)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new UserNotFoundException(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            var userDto = _mapper.Map(updatedUser, user);
            var result = await _userManager.UpdateAsync(userDto);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, updatedUser.Roles);
            }
        }


        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException(userId);

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;


        }

    }
}
