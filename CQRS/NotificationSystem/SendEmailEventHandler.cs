using MediatR;
using static Alachisoft.NCache.Common.Threading.AsyncProcessor;

namespace CQRS.NotificationSystem
{
    public class SendEmailEventHandler(ILogger<SendEmailEventHandler> logger) : INotificationHandler<PlayerCreatedEvent>
    {
        async Task INotificationHandler<PlayerCreatedEvent>.Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"email - player creation started.");
            await Task.Delay(1000);
            logger.LogInformation($"email - player creation ended.");
        }
    }
}
