using System.Diagnostics;

namespace CQRS.DesignPattern.Structural.Decorator.Exam1
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public interface IPlayersService
    {
        IEnumerable<Player> GetPlayersList();
    }
    public class PlayersService : IPlayersService
    {
        public IEnumerable<Player> GetPlayersList()
        {
            return new List<Player>()
            {
                new Player(){ Id = 1, Name = "Juan Mata" },
                new Player(){ Id = 2, Name = "Paul Pogba" },
                new Player(){ Id = 3, Name = "Phil Jones" },
                new Player(){ Id = 4, Name = "David de Gea" },
                new Player(){ Id = 5, Name = "Marcus Rashford" }
            };
        }
    }
    public class PlayersServiceLoggingDecorator : IPlayersService
    {
        private readonly IPlayersService _playersService;
        private readonly ILogger<PlayersServiceLoggingDecorator> _logger;

        public PlayersServiceLoggingDecorator(IPlayersService playersService,
            ILogger<PlayersServiceLoggingDecorator> logger)
        {
            _playersService = playersService;
            _logger = logger;
        }

        public IEnumerable<Player> GetPlayersList()
        {
            _logger.LogInformation("Starting to fetch data");

            var stopwatch = Stopwatch.StartNew();

            IEnumerable<Player> players = _playersService.GetPlayersList();

            foreach (var player in players)
            {
                _logger.LogInformation("Player: " + player.Id + ", Name: " + player.Name);
            }

            stopwatch.Stop();

            var elapsedTime = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation($"Finished fetching data in {elapsedTime} milliseconds");

            return players;
        }
    }
}
