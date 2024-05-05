using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.AuditLog;

namespace Service
{
    internal sealed class AuditService : IAuditService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AuditService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(bool trackChanges)
        {
            var audits = await _repository.Audit.GetAuditLogsAsync(trackChanges);

            var auditsDto = _mapper.Map<IEnumerable<AuditLogDto>>(audits);

            return auditsDto;
        }
    }
}
