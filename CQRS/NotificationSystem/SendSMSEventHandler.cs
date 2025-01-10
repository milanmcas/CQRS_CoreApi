using MediatR;

namespace CQRS.NotificationSystem
{
    public class SendSMSEventHandler(ILogger<SendSMSEventHandler> logger) : INotificationHandler<PlayerCreatedEvent>
    {
        public async Task Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"SMS - player creation started.");
            await Task.Delay(1000);
            logger.LogInformation($"SMS - player creation ended.");
        }
    }
}
