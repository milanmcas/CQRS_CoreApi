using MediatR;

namespace CQRS.NotificationSystem
{
    public class SendThirdPartyApiEvenHandler(ILogger<SendThirdPartyApiEvenHandler> logger) : INotificationHandler<PlayerCreatedEvent>
    {
        async Task INotificationHandler<PlayerCreatedEvent>.Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"ThirdParty - player creation started.");
            await Task.Delay(1000);
            logger.LogInformation($"ThirdParty - player creation ended.");
        }
    }
}
