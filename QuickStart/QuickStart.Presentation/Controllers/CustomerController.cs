using Microsoft.AspNetCore.Mvc;
using QuickStart.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.Customer;

namespace QuickStart.Presentation.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CustomerController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _service.CustomerService.GetAllCustomersAsync(trackChanges: false);

            return Ok(customers);
        }

        [HttpGet("{id:guid}", Name = "CustomerById")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _service.CustomerService.GetCustomerAsync(id, trackChanges: false);
            return Ok(customer);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreationDto customer)
        {
            var createdCustomer = await _service.CustomerService.CreateCustomerAsync(customer);

            return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _service.CustomerService.DeleteCustomerAsync(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerForUpdateDto customer)
        {
            await _service.CustomerService.UpdateCustomerAsync(id, customer, trackChanges: true);

            return NoContent();
        }
    }
}
