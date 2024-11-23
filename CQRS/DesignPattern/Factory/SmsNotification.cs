namespace CQRS.DesignPattern.Factory
{
    public class SmsNotification: INotification
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Sending SMS to {to}: {message}");
        }
    }
}
