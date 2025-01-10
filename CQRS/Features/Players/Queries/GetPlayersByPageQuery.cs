using CQRS.Models;
using CQRS.Services;
using MediatR;

namespace CQRS.Features.Players.Queries
{
    public class GetPlayersByPageQuery : IRequest<IEnumerable<Player>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        
    }
    public class GetPlayersByPageQueryHandler : IRequestHandler<GetPlayersByPageQuery, IEnumerable<Player>>
    {
        private readonly IPlayerService _playerService;

        public GetPlayersByPageQueryHandler(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IEnumerable<Player>> Handle(GetPlayersByPageQuery query, CancellationToken cancellationToken)
        {
            return await _playerService.GetPlayersByPage(query.PageIndex,query.PageSize);
        }
    }
}
