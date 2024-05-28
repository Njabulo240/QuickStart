namespace Entities.Exceptions.Authentication
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userId)
            : base($"User with id: {userId} doesn't exist in the database.")
        {
        }
    }
}
