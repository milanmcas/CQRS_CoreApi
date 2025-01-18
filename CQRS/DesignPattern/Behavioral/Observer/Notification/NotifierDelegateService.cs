namespace CQRS.DesignPattern.Behavioral.Observer.Notification
{
    //public delegate void NotifyObserver(string message);
    public class NotifierDelegateService : INotifierService
    {
        private delegate void NotifyObserver(string message);
        private event NotifyObserver NotifyObserverEvent;
        void INotifierService.Attach(IMessageNotificationService subscriber)
        {
            if (subscriber != null)
                NotifyObserverEvent += subscriber.Send;
            //NotifyObserverEvent = null;
        }

        void INotifierService.Detach(IMessageNotificationService subscriber)
        {
            if (subscriber != null)
                NotifyObserverEvent -= subscriber.Send;
        }

        void INotifierService.Notify(string message)
        {
            if (NotifyObserverEvent != null)
                NotifyObserverEvent(message);
        }
    }
}
