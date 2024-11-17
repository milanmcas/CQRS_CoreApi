using CQRS.Models;
using CQRS.Services;
using MediatR;

namespace CQRS.Features.Players.Queries
{
    public class GetAllPlayersQuery : IRequest<IEnumerable<Player>>
    {
        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<Player>>
        {
            private readonly IPlayerService _playerService;

            public GetAllPlayersQueryHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<IEnumerable<Player>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
            {
                return await _playerService.GetPlayersList();
            }
        }
    }
}
