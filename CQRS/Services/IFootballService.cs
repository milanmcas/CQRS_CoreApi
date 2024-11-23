using CQRS.FootballModels;

namespace CQRS.Services
{
    public interface IFootballService
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
    }
}
