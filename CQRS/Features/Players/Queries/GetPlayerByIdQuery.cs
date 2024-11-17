using CQRS.Models;
using CQRS.Services;
using MediatR;

namespace CQRS.Features.Players.Queries
{
    public class GetPlayerByIdQuery : IRequest<Player>
    {
        public int Id { get; set; }

        public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Player>
        {
            private readonly IPlayerService _playerService;

            public GetPlayerByIdQueryHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<Player> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
            {
                return await _playerService.GetPlayerById(query.Id);
            }
        }
    }
}
