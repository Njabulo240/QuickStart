namespace Entities.Exceptions.Authentication
{
    public class TokenBadRequest : BadRequestException
    {
        public TokenBadRequest()
        : base("Bad request")

        {
        }
    }
}
