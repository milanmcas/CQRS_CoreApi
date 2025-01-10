using CQRS.Models;

namespace CQRS.Data.Repository
{
    public interface IOnlineShopRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        IQueryable<Order> GetOnlineOrders();
        IQueryable<Product> GetProduct();
    }
}
