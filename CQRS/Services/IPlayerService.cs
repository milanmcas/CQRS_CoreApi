using CQRS.Models;

namespace CQRS.Services
{
    public interface IPlayerService
    {
        IQueryable<Product> GetProducts();
        Task<IEnumerable<Player>> GetPlayersByPage(int pageIndex, int pageSize);
        IQueryable<Student> GetStudents();
        Task<IEnumerable<Player>> GetPlayersList();
        Task<Player> GetPlayerById(int id);
        Task<Player> CreatePlayer(Player player);
        Task<List<Player>> CreatePlayers(List<Player> players);
        Task<List<Player>> CreateBulkTablePlayers(List<Player> players);
        Task<int> UpdatePlayer(Player player);
        Task<int> DeletePlayer(Player player);
    }
}
