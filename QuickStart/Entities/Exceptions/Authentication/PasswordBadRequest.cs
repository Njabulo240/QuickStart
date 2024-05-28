namespace Entities.Exceptions.Authentication
{
    public class PasswordBadRequest : BadRequestException
    {
        public PasswordBadRequest()
        : base("The previous password is invalid.")

        {
        }
    }
}
