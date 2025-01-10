using Alachisoft.NCache.Client.Services;
using CQRS.DesignPattern.Behavioral.Observer.Notification;
using INotificationService = CQRS.DesignPattern.Behavioral.Observer.Notification.IMessageNotificationService;

namespace CQRS.DesignPattern.Factory
{
    public class ObserverFactory
    {
        
        public IMessageNotificationService CreateNotification(NotificationTypes notification)
        {
            switch (notification)
            {
                case NotificationTypes.Mail:
                    return new EmailNotificationService();
                case NotificationTypes.SMS:
                    return new SMSNotificationService();
                case NotificationTypes.ThirdPartyAPI:
                    return new ThirdpartApiNotificationService();
                    default: return new EmailNotificationService();
            }
        } 
    }
}
