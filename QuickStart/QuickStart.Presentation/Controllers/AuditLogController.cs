using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.AuditLog;

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

        [HttpGet("GetMonths")]
        public async Task<IActionResult> GetMonthAsync(bool trackChanges = false)
        {
            var result = await _service.AuditService.GetMonthAsync(trackChanges);
            return Ok(result);

        }

        [HttpGet("totalForSingleMonth/{monthYear}")]
        public async Task<ActionResult<MonthTotalDto>> GetTotalForSingleMonth(string monthYear)
        {
            var result = await _service.AuditService.GetTotalAuditForSingleMonthAsync(monthYear, trackChanges: false);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
