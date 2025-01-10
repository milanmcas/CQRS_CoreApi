using System.Runtime.Serialization;

namespace CQRS.DesignPattern.Behavioral.Observer.Notification
{
    /// <summary>
    /// Observers: They are also called subscribers. They listen to the changes in the subjects.
    /// </summary>
    public interface IMessageNotificationService
    {
        void Send(string message);
    }
    public class EmailNotificationService : IMessageNotificationService
    {
        void IMessageNotificationService.Send(string message)
        {
            Console.WriteLine($"Email Notification - {message}");
        }
    }
    public class SMSNotificationService : IMessageNotificationService
    {
        void IMessageNotificationService.Send(string message)
        {
            Console.WriteLine($"SMS Notification - {message}");
        }
    }
    public class ThirdpartApiNotificationService : IMessageNotificationService
    {
        void IMessageNotificationService.Send(string message)
        {
            Console.WriteLine($"ThirdpartApi Notification - {message}");
        }
    }
    public enum NotificationTypes
    {
        [EnumMember]
        SMS = 1,

        [EnumMember]
        Mail = 2,

        [EnumMember]
        ThirdPartyAPI = 3
    }

}
