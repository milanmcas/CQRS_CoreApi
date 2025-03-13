using CQRS.DesignPattern.Factory;

namespace CQRS.DesignPattern.Behavioral.Observer.Notification
{
    /// <summary>
    /// Subject: They are also called Publishers. When a change occurs to a subject, it should notify all its Subscribers/Observers.
    /// </summary>
    public interface INotifierService
    {
        void Attach(IMessageNotificationService subscriber);
        void Detach(IMessageNotificationService subscriber);
        void Notify(string message);
    }
    public class NotifierService : INotifierService
    {
        private List<IMessageNotificationService> _subscribers = new List<IMessageNotificationService>();
        private string _news="new thing";
        void INotifierService.Attach(IMessageNotificationService subscriber)
        {
            _subscribers.Add(subscriber);
        }
        void INotifierService.Detach(IMessageNotificationService subscriber)
        {
            _subscribers.Remove(subscriber);
        }
        void INotifierService.Notify(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Send(message);
            }
        }
    }
    public class Notifier
    {
        private readonly INotifierService _subject;
        private readonly ObserverFactory _notificationFactory;
        List<NotificationTypes> _observerList;
        public Notifier(INotifierService subject, ObserverFactory notificationFactory)
        {
            _subject = subject;
            _notificationFactory = notificationFactory;
            _observerList = new List<NotificationTypes>() { NotificationTypes.ThirdPartyAPI, NotificationTypes.SMS,NotificationTypes.Mail };
        }
        public void Process(string message,List<NotificationTypes>? observerList=null)
        {
            if (observerList == null)
                observerList = _observerList;
                foreach (var notificationType in observerList)
                {
                IMessageNotificationService nobject = _notificationFactory.CreateNotification(notificationType);
                    _subject.Attach(nobject);
                }           

            SendNotification(message);
        }
        public void SendNotification(string message)
        {
            // Send notification logic...

            // Notify observers
            _subject.Notify(message);
        }
    }
}
