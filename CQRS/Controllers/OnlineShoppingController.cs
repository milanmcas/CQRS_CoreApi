using Alachisoft.NCache.Web.Caching;
using CQRS.Data;
using CQRS.Data.Repository;
using CQRS.Extensions;
using CQRS.Models;
using CQRS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.ObjectPool;
using System.Data;
using System.Data.Common;
using System.Runtime.Versioning;
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
        //[HttpGet]
        //public IhttpActionResult getd()
        //{

        //}
        [HttpGet("get/emp/salary")]
        public IEnumerable<EmpSalary> GetEmpSalaries()
        {
            var list=_onlineShopRepository.GetEmpSalaries();
            return list;
        }
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

            using (var transaction = _sampleCpntext.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
            {
                try
                {
                    // Perform transactional operations here

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return "value";
        }
        [HttpGet("get/product-categories")]
        public async Task<ProductModel> getProductModels()
        {
            var prodModel = new ProductModel();
            
            
            try
            {
                var aa= _sampleCpntext.productNews.FromSql($"USP_GetMultiSetProducts").ToList();
                //var prodModelList = GetStoredProcedure("USP_GetMultiSetProducts")
                //.ExecuteStoredProcedure<product_category>();
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
        [HttpPost("add/blog")]
        public async Task<ActionResult<Blog>> AddBlog(Blog blog)
        {
            try
            {
                //var blogs= _sampleCpntext.Blogs.ToList();
                await _sampleCpntext.Blogs.AddAsync(blog);
            }
            catch (Exception ex) { 
                Console.WriteLine($"Error {ex.ToString()}");
            }
            
            return Ok(blog);
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
        [HttpPost("add/product1")]
        public async ValueTask<int> InsertProduct1(productNew product)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            { 
                // Create parameters    
                new SqlParameter { ParameterName = "@product_id", Value = product.product_id },
                new SqlParameter { ParameterName = "@product_name", Value = product.product_name },
                new SqlParameter { ParameterName = "@date_added", Value = product.date_added },
                new SqlParameter { ParameterName = "@product_category_id", Value = product.product_category_id }
            };
            try
            {
                string sql = "exec USP_AddProduct @product_id,@product_name,@date_added,@product_category_id";
                //return await _sampleCpntext.Database.ExecuteSqlRawAsync(sql, parms.ToArray());
                //return await _sampleCpntext.Database.ExecuteSqlAsync($"exec USP_AddProduct {parms.ToArray()}");
                return await _sampleCpntext.Database.ExecuteSqlAsync($"exec USP_AddProduct @product_id={product.product_id},@product_name={product.product_name},@date_added={product.date_added},@product_category_id={product.product_category_id}");
            }
            catch (Exception ex)
            {
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
        //[HttpPost]
        //public JsonResult GetFruitName(string id)
        //{
        //    string procedureName = "dbo.GetFruitName @FruitId, @FruitName OUT";
        //    SqlParameter sqlParameter = new SqlParameter("@FruitId", id);
        //    var sqlParameterOut = new SqlParameter
        //    {
        //        ParameterName = "@FruitName",
        //        DbType = DbType.String,
        //        Size = 30,
        //        Direction = ParameterDirection.Output
        //    };
        //    var fruit = _sampleCpntext.Database.ExecuteSqlCommand(procedureName, sqlParameter, sqlParameterOut);
        //    string name = Convert.ToString(sqlParameterOut.Value);

        //    return Json(name);
        //}
        // POST api/<OnlineShoppingController>
        [SupportedOSPlatform("windows")]
        //[SupportedOSPlatform("ios14.0")]
        //[SupportedOSPlatform("linux")]
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            //Object Pooling
            // Example of object pooling
            //ObjectPool<Blog> pool = new ObjectPool<Blog>(() => new Blog());

            // Get an object from the pool
            //Blog obj = pool.Get();

            // Use the object

            // Return the object to the pool
            //pool.Return(obj);
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
                //_sampleCpntext.Database.BeginTransaction();
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
        private DbCommand GetStoredProcedure(string name, params (string,object)[] nameValueParams)
        {
            return _sampleCpntext
                .LoadStoredProcedure(name)
                .WithSqlParams(nameValueParams);
        }
        private DbCommand GetStoredProcedure(string name)
        {
            return _sampleCpntext
                .LoadStoredProcedure(name);
                
        }
    }
}
