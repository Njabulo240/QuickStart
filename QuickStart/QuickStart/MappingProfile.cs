using AutoMapper;
using Entities.Identity;
using Entities.Models;
using Shared.DataTransferObjects.Account;
using Shared.DataTransferObjects.AuditLog;
using Shared.DataTransferObjects.Authentication;
using Shared.DataTransferObjects.Customer;
using Shared.DataTransferObjects.User;
using Shared.DataTransferObjects.UserRole;

namespace QuickStart
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Customer, CustomerDto>()
                .ForMember(d => d.accountCount, o => o.MapFrom(s => s.Accounts.Count));
            CreateMap<Account, AccountDto>();
            CreateMap<AccountForCreationDto, AccountDto>();
            CreateMap<CustomerForCreationDto, Customer>();
            CreateMap<AccountForCreationDto, Account>();
            CreateMap<AccountForUpdateDto, Account>().ReverseMap();
            CreateMap<CustomerForUpdateDto, Customer>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserRoleForCreationDto, UserRole>();
            CreateMap<UserRoleForUpdateDto, UserRole>();
            CreateMap<AuditLog, AuditLogDto>();
        }
    }
}
