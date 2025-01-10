using MediatR;

namespace CQRS.NotificationSystem
{
    public class SendPushNotificationHandler(ILogger<SendPushNotificationHandler> logger) : INotificationHandler<PlayerCreatedEvent>
    {
        public async Task Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Push - player creation started.");
            await Task.Delay(1000);
            logger.LogInformation($"Push - player creation ended.");
        }
    }
}
