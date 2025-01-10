using MediatR;

namespace CQRS.NotificationSystem
{
    public class PlayerCreatedEvent1:INotification
    { 
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public record PlayerCreatedEvent(int Id, string Name) : INotification
    {

    }
}
