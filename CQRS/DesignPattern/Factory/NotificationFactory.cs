namespace CQRS.DesignPattern.Factory
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider serviceProvider;
        public NotificationFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        INotification INotificationFactory.CreateNotification(string type)
        {
            switch (type.ToLower()) {
                case "email":
                        return (INotification)serviceProvider.GetService(typeof(EmailNotification));
                    
                case "push":
                    return (INotification)serviceProvider.GetService(typeof(PushNotification));
                case "sms":
                    return (INotification)serviceProvider.GetService(typeof(SmsNotification));
                    default:
                    throw new InvalidOperationException("Invalid notification type");
            }
            //_ => throw new InvalidOperationException("Invalid notification type");
        }
    }
}
