namespace CQRS.DesignPattern.Structural.Decorator.Notification
{
    public interface INotification
    {
        void Send(string message);
    }
    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"decorator - Sending email: {message}");
        }
    }
    public abstract class NotificationDecorator : INotification
    {
        protected INotification _notification;

        public NotificationDecorator(INotification notification)
        {
            _notification = notification;
        }

        public abstract void Send(string message);
    }
    public class SMSNotification : NotificationDecorator
    {
        public SMSNotification(INotification notification) : base(notification) { }

        public override void Send(string message)
        {
            _notification.Send(message);
            Console.WriteLine($"decorator - Sending SMS: {message}");
        }
    }
    public class ThirdpartyAPINotification : NotificationDecorator
    {
        public ThirdpartyAPINotification(INotification notification) : base(notification) { }

        public override void Send(string message)
        {
            _notification.Send(message);
            Console.WriteLine($"decorator - Sending SMS: {message}");
        }
    }
}
