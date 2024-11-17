using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Data.Repository
{
    public class OnlineShopRepository : IOnlineShopRepository
    {
        private readonly OnlineShopDbContext _context;
        public OnlineShopRepository(OnlineShopDbContext context) 
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }
        async Task<IEnumerable<Order>> IOnlineShopRepository.GetOrders()
        {
            return await _context.Orders
                .ToListAsync(); ;
        }
    }
}
