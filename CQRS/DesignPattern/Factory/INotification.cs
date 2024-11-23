namespace CQRS.DesignPattern.Factory
{
    public interface INotification
    {
        void Send(string to, string message);
    }
}
