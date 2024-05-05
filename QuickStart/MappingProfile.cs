using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.AuditLog;

namespace QuickStart
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuditLog, AuditLogDto>();
        }
    }
}
