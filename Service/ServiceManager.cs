using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuditService> _auditService;
        public ServiceManager(IRepositoryManager repositoryManager,
              ILoggerManager logger, IMapper mapper)
        {

            _auditService = new Lazy<IAuditService>(() => new AuditService(repositoryManager, logger, mapper));

        }

        public IAuditService AuditService => _auditService.Value;
    }
}
