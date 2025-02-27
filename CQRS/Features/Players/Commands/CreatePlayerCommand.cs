﻿using CQRS.Models;
using CQRS.Services;
using MediatR;

namespace CQRS.Features.Players.Commands
{
    public class CreatePlayerCommand : IRequest<Player>
    {
        public int? ShirtNo { get; set; }
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }
        public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Player>
        {
            private readonly IPlayerService _playerService;

            public CreatePlayerCommandHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }
            public async Task<Player> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
            {
                var player = new Player()
                {
                    ShirtNo = command.ShirtNo,
                    Name = command.Name,
                    Appearances = command.Appearances,
                    Goals = command.Goals
                };

                return await _playerService.CreatePlayer(player);
            }
        }
    }
}
