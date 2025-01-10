using CQRS.Models;
using CQRS.Services;
using MediatR;

namespace CQRS.Features.Players.Commands
{
    public class CreatePlayersCommand:IRequest<List<Player>>
    {
       public List<PostPlayer> PostPlayers { get; set; }
        public class CreatePlayersCommandHandler : IRequestHandler<CreatePlayersCommand, List<Player>>
        {
            private readonly IPlayerService _playerService;

            public CreatePlayersCommandHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }
            public async Task<List<Player>> Handle(CreatePlayersCommand command, CancellationToken cancellationToken)
            {
                List<Player> players = new List<Player>();
                foreach (Player player in players)
                {
                    players.Add(new Player() { Name=player.Name,
                        ShirtNo=player.ShirtNo,
                    Appearances=player.Appearances,
                    Goals=player.Goals});
                }

                return await _playerService.CreatePlayers(players);
            }
        }
    }
}
