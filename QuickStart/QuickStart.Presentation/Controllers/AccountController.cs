using Microsoft.AspNetCore.Mvc;
using QuickStart.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.Account;

namespace QuickStart.Presentation.Controllers
{
    [Route("api/customers/{customerId}/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AccountController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAccountForCustomer(Guid customerId)
        {
            var accounts = await _service.AccountService.GetAccountsAync(customerId, trackChanges: false);
            return Ok(accounts);
        }

        [HttpGet("{id:guid}", Name = "GetAccountForCustomer")]
        public async Task<IActionResult> GetAccountForCustomer(Guid customerId, Guid id)
        {
            var accounts = await _service.AccountService.GetAccountAsync(customerId, id, trackChanges: false);
            return Ok(accounts);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAccountForCustomer(Guid customerId, [FromBody] AccountForCreationDto account)
        {
            var accountToReturn = await _service.AccountService.CreateAccountForCustomerAsync(customerId, account,
                trackChanges: false);

            return CreatedAtRoute("GetAccountForCustomer", new { customerId, id = accountToReturn.Id }, accountToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAccountForCustomer(Guid customerId, Guid id)
        {
            await _service.AccountService.DeleteAccountForCustomerAsync(customerId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAccountForCustomer(Guid customerId, Guid id, [FromBody] AccountForUpdateDto account)
        {
            await _service.AccountService.UpdateAccountForCustomerAsync(customerId, id, account, cusTrackChanges: false, accTrackChanges: true);

            return NoContent();
        }


    }
}