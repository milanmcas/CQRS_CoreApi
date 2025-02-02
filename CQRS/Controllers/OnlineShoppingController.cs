using Alachisoft.NCache.Web.Caching;
using CQRS.Data;
using CQRS.Data.Repository;
using CQRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Text.Json;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineShoppingController : ControllerBase
    {
        private readonly IOnlineShopRepository _onlineShopRepository;
        private readonly OnlineShopDbContext _onlineShopDbContext;
        private readonly SampleCpntext _sampleCpntext;
        private readonly IDistributedCache _distributedCache;
        public OnlineShoppingController(
            IOnlineShopRepository onlineShopRepository,
            OnlineShopDbContext onlineShopDbContext,
            IDistributedCache distributedCache,
            SampleCpntext sampleCpntext)
        {
            _onlineShopRepository = onlineShopRepository;

            _onlineShopDbContext = onlineShopDbContext;
            _sampleCpntext = sampleCpntext;
            _distributedCache= distributedCache;
        }
        // GET: api/<OnlineShoppingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var id = 1;
            var query = _onlineShopDbContext.Products
                .FromSqlInterpolated($"select * from Product where id={id}");
            var query1 = _onlineShopDbContext.Products
                .FromSql($"select * from Product where id={id}");
            //var query2 = _onlineShopDbContext.Products
            //    .FromSqlRaw("select * from Product where id={id}");
            var query3 = _onlineShopDbContext.Products
                .FromSqlRaw("select * from Product where id={0}", id);
            var queryString1 = query.ToQueryString();
            var queryString2 = query1.ToQueryString();
            //var queryString3 = query2.ToQueryString();
            var queryString4 = query3.ToQueryString();
            var result= _onlineShopRepository.GetProduct();
            return new string[] { "value1", "value2" };
        }

        // GET api/<OnlineShoppingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //_sampleCpntext.Database.ExecuteSql($"exec proc");//returns no of rows affected.
            //_sampleCpntext.Database.SqlQuery<string>($"exec proc");//returns scalar value
            //_sampleCpntext.City.FromSql($"exec proc");//returns entity
            return "value";
        }
        [HttpGet("get/product-categories")]
        public async Task<ProductModel> getProductModels()
        {
            var prodModel = new ProductModel();
            try
            {
                
                using (var connection = _sampleCpntext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using var command = connection.CreateCommand();
                    command.CommandText = "USP_GetMultiSetProducts";
                    command.CommandType = CommandType.Text;
                    using var reader = await command.ExecuteReaderAsync();
                    var productList = new List<productNew>();
                    var catList = new List<product_category>();
                    while (await reader.ReadAsync())
                    {
                        productList.Add(new productNew()
                        {
                            product_id = reader.GetInt32("product_id"),
                            product_name = reader.GetString("product_name"),
                            date_added = DateOnly.FromDateTime(reader.GetDateTime("date_added")),
                            product_category_id = reader.GetInt32("product_category_id")
                        });
                    }
                    prodModel.Products = productList;
                    await reader.NextResultAsync();
                    while (await reader.ReadAsync())
                    {
                        catList.Add(new product_category()
                        {
                            cat_id = reader.GetInt32("cat_id"),
                            cat_name = reader.GetString("cat_name")
                        });
                    }
                    prodModel.ProductCategory = catList;
                }
            }
            catch(Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
            
            return prodModel;
        }
        [HttpPost("add/product")]
        public async ValueTask<int> InsertProduct(productNew product)
        {
            try
            {
                return await _sampleCpntext.Database.ExecuteSqlAsync($"USP_AddProduct @product_id={product.product_id},@product_name={product.product_name},@date_added={product.date_added},@product_category_id={product.product_category_id}");
                //return await _sampleCpntext.Database.ExecuteSqlAsync($"exec USP_AddProduct @product_id={product.product_id},@product_name={product.product_name},@date_added={product.date_added},@product_category_id={product.product_category_id}");
            }
            catch (Exception ex) {
                return 0;
            }            
        }
        [HttpGet("get/products1")]
        public IQueryable<productNew> GetProducts1()
        {
            return _sampleCpntext.productNews.FromSql($"exec USP_GetProduct");
        }
        [HttpGet("get/products")]
        public async Task<List<productNew>> GetProducts()
        {
            //_sampleCpntext.productNews.FromSqlRaw($"exec USP_GetProduct");
            return await _sampleCpntext.productNews.FromSql($"exec USP_GetProduct").ToListAsync();
        }
        [HttpGet("get/products1/{id}")]
        public productNew? GetProductsById1(int id)
        {
            try
            {
                return _sampleCpntext.productNews.FromSqlInterpolated($"USP_GetProductById {id}").ToList().FirstOrDefault();
                    
                //return await _sampleCpntext.productNews.FromSql($"USP_GetProductById {id}").ToListAsync();

                //return await _sampleCpntext.productNews.FromSqlRaw("USP_GetProductById @product_id", new SqlParameter("@product_id", id)).ToListAsync();
            }
            catch (Exception ex)
            {
                return new productNew();
            }

            //return await _sampleCpntext.productNews.FromSql($"exec USP_GetProductById {id}")
            //    .FirstOrDefaultAsync()??new productNew();
        }
        [HttpGet("get/products/{id}")]
        public async Task<List<productNew>> GetProductsById(int id)
        {
            try
            {
                return await _sampleCpntext.productNews.FromSqlInterpolated($"USP_GetProductById {id}").ToListAsync();
                //return await _sampleCpntext.productNews.FromSql($"USP_GetProductById {id}").ToListAsync();

                //return await _sampleCpntext.productNews.FromSqlRaw("USP_GetProductById @product_id", new SqlParameter("@product_id", id)).ToListAsync();
            }
            catch (Exception ex) { 
                return new List<productNew>();
            }
            
            //return await _sampleCpntext.productNews.FromSql($"exec USP_GetProductById {id}")
            //    .FirstOrDefaultAsync()??new productNew();
        }
        // POST api/<OnlineShoppingController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            try
            {
                var aa = new { name = "milan" };
                _distributedCache.Refresh("key");
                //_distributedCache.Set("", JsonSerializer.Serialize(value), new DistributedCacheEntryOptions()
                //{ AbsoluteExpiration = DateTime.Now.AddMinutes(5) }
                //);
                //SqlCacheDependancy
                //CacheItemRemovedCallback
                //var contact=new Contact()
                //{
                //    ContactId=value.ContactId,
                //    FirstName=value.FirstName,
                //    LastName=value.LastName
                //};
                // Enable implicit distributed transactions in case operations span multiple databases
                TransactionManager.ImplicitDistributedTransactions = true;
                // Define transaction options: Isolation level (ReadCommitted) and timeout duration (default 1 minute)
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, // Ensures data read is committed, avoiding dirty reads
                    Timeout = TransactionManager.DefaultTimeout // Default transaction timeout
                };
                using (var scope = new TransactionScope(
                TransactionScopeOption.Required, // Requires a new transaction or joins an existing one
                transactionOptions,
                TransactionScopeAsyncFlowOption.Enabled))
                {
                    //_sampleCpntext.Database.ExecuteSql($"exec proc");//returns no of rows affected.
                    //_sampleCpntext.Database.SqlQuery<string>($"exec proc");//returns scalar value
                    //_sampleCpntext.City.FromSql($"exec proc");
                    _sampleCpntext.Contact.Add(value);
                    _sampleCpntext.SaveChanges();
                    // 3. Mark the transaction as complete
                    scope.Complete();
                }
                    
                return Ok();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }

        // PUT api/<OnlineShoppingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OnlineShoppingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
