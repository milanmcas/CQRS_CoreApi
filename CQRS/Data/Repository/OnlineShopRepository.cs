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
                .ToListAsync();
            //_context.Database.ExecuteSql()
            //_context.Database.
        }
        public IQueryable<Order> GetOnlineOrders()
        {
            return _context.Orders;
               
        }
        public IQueryable<Product> GetProduct()
        {
            var id = 1;
            var query=_context.Products
                .FromSqlInterpolated($"select * from Product where id={id}");
            var query1 = _context.Products
                .FromSql($"select * from Product where id={id}");
            //var query2 = _context.Products
            //    .FromSqlRaw("select * from Product where id={id}");
            var query3 = _context.Products
                .FromSqlRaw("select * from Product where id={0}",id);
            var queryString1= query.ToQueryString();
            var queryString2 = query1.ToQueryString();
            //var queryString3 = query2.ToQueryString();
            var queryString4 = query3.ToQueryString();

            var query11 = _context.Products
                .FromSqlInterpolated($"select * from Product where id=1");
            var query12 = _context.Products
                .FromSql($"select * from Product where id=1");
            var query13 = _context.Products
                .FromSqlRaw("select * from Product where id=1");
            var queryString11 = query11.ToQueryString();
            var queryString12 = query12.ToQueryString();
            var queryString13 = query13.ToQueryString();
            return _context.Products;
        }
    }
}
