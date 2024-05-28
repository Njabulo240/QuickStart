namespace Entities.Exceptions.Notification
{
    public sealed class NotificationNotFoundException : NotFoundException
    {
        public NotificationNotFoundException(Guid Id)
            : base($"The notification with id: {Id} doesn't exist in the database.")
        {
        }
    }
}
