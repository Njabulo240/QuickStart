namespace Entities.Exceptions.Account
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(Guid accountId)
            : base($"Account with id: {accountId} doesn't exist in the database.")
        {
        }
    }
}
