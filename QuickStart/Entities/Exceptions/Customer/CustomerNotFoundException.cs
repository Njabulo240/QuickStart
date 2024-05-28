namespace Entities.Exceptions.Customer
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(Guid customerId)
            : base($"The customer with id: {customerId} doesn't exist in the database.")
        {
        }
    }
}
