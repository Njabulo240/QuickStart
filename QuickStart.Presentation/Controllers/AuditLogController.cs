using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace QuickStart.Presentation.Controllers
{
    [Route("api/audits")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuditLogController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAudits()
        {
            var audits = await _service.AuditService.GetAuditLogsAsync(trackChanges: false);
            return Ok(audits);
        }

    }
}
