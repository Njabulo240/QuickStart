namespace Entities.Exceptions.Authentication
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string roleId)
            : base($"The role with id: {roleId} doesn't exist in the database.")
        {
        }
    }
}
