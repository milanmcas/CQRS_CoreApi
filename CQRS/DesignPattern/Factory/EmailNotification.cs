namespace CQRS.DesignPattern.Factory
{
    public class EmailNotification : INotification
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Sending Email to {to}: {message}");
        }
    }
}
