using CQRS.Data;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Microsoft.Data.SqlClient;

namespace CQRS.Services
{
    public class PlayerService:IPlayerService
    {
        private readonly PlayerDbContext _context;

        public PlayerService(PlayerDbContext context)
        {
            _context = context;
        }
        public IQueryable<Student> GetStudents()
        {
            return _context.Student.Include(x=>x.Books);
        }
        public IQueryable<Product> GetProducts() {
            return  _context.Products;
        }
        public async Task<IEnumerable<Player>> GetPlayersList()
        {
            //IQueryable<Player> query = _context.Players.Take(2);
            //IEnumerable<Player> activeEntities = query.ToList();
            return await _context.Players
                .ToListAsync();
        }
        //Paging and filtering
        public async Task<IEnumerable<Player>> GetPlayersByPage(int pageIndex, int pageSize)
        {
            var list= _context.Players
                .OrderBy(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return await _context.Players
                .OrderBy(x=>x.Id)
                .Skip((pageIndex-1)* pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Player> GetPlayerById(int id)
        {
            return await _context.Players
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }
        public async Task<List<Player>> CreatePlayers(List<Player> players)
        {
            _context.Players.AddRange(players);
            //_context.Database.ExecuteSql();
            await _context.SaveChangesAsync();
            return players;
        }
        public async Task<List<Player>> CreateBulkTablePlayers(List<Player> players)
        {
            var playersTable = new DataTable();
            playersTable.Columns.Add("Id", typeof(Int32));
            playersTable.Columns.Add("ShirtNo", typeof(Int32));
            playersTable.Columns.Add("Name", typeof(string));
            playersTable.Columns.Add("Appearances", typeof(Int32));
            playersTable.Columns.Add("Goals", typeof(Int32));
            var i = 0;
            foreach (var player in players) {
                playersTable.Rows.Add(i++,player.ShirtNo,
                    player.Name,player.Appearances,player.Goals);
            }
            SqlParameter tableParam=new SqlParameter("@Player", SqlDbType.Structured);
            tableParam.TypeName = "dbo.PlayerType";
            tableParam.Value = playersTable;

            await _context.Database.ExecuteSqlAsync($"USP_InsertPlayer {tableParam}");
            //_context.Database.ExecuteSql();
            await _context.SaveChangesAsync();
            return players;
        }
        public async Task<List<Player>> CreateBulkPlayers(List<Player> players)
        {
            _context.Players.AddRange(players);
            await _context.SaveChangesAsync();
            return players;
        }

        public async Task<int> UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
            return await _context.SaveChangesAsync();
        }
    }
}
