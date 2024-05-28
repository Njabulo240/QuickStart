using AutoMapper;
using Contracts;
using Entities.Exceptions.Customer;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Customer;

namespace Service
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }



        public async Task<CustomerDto> CreateCustomerAsync(CustomerForCreationDto customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);

            _repository.Customer.CreateCustomer(customerEntity);
            await _repository.SaveAsync();

            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return customerToReturn;
        }

        public async Task DeleteCustomerAsync(Guid customerId, bool trackChanges)
        {
            var customer = await GetCustomerAndCheckIfItExists(customerId, trackChanges);

            _repository.Customer.DeleteCustomer(customer);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(bool trackChanges)
        {
            var customers = await _repository.Customer.GetCustomersAsync(trackChanges);

            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customersDto;
        }

        public async Task<CustomerDto> GetCustomerAsync(Guid customerId, bool trackChanges)
        {
            var customer = await GetCustomerAndCheckIfItExists(customerId, trackChanges);

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task UpdateCustomerAsync(Guid customerId, CustomerForUpdateDto customerForUpdate, bool trackChanges)
        {
            var customer = await GetCustomerAndCheckIfItExists(customerId, trackChanges);

            _mapper.Map(customerForUpdate, customer);
            await _repository.SaveAsync();
        }


        private async Task<Customer> GetCustomerAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(id, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(id);

            return customer;
        }
    }
}
