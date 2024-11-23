using CQRS.Data;
using CQRS.FootballModels;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Services
{
    public class FootballService : IFootballService
    {
        private readonly SportsDbContext _context;
        public FootballService(SportsDbContext context)
        {
            _context = context;
        }
        async Task<IEnumerable<Player>> IFootballService.GetPlayersAsync()
        {
            return await _context.Players
            .Include(x => x.Position)
            .ToListAsync();
        }
    }
}
