namespace Entities.Exceptions.Authentication
{
    public class EmailBadRequest : BadRequestException
    {
        public EmailBadRequest()
        : base("The user is invalid.")

        {
        }
    }
}
