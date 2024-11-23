namespace CQRS.DesignPattern.Factory
{
    public interface INotificationFactory
    {
        INotification CreateNotification(string type);
    }
}
