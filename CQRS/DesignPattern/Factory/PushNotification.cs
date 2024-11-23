namespace CQRS.DesignPattern.Factory
{
    public class PushNotification: INotification
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Sending Push Notification to {to}: {message}");
        }
    }
}
