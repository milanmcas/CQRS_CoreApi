using CQRS.FootballModels;

namespace CQRS.Services
{
    public class Query
    {
        public async Task<IEnumerable<Player>> GetPlayersAsync(
        [Service] IFootballService playerService)
        {
            return await playerService.GetPlayersAsync();
        }
    }
}
