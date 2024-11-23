namespace CQRS.DesignPattern.Factory
{
    public class Factory : INotificationFactory
    {
        INotification INotificationFactory.CreateNotification(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailNotification(),
                "sms" => new SmsNotification(),
                _ => new PushNotification(),
            };
        }
    }
}
