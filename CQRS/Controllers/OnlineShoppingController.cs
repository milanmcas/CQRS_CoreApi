using CQRS.Data;
using CQRS.Data.Repository;
using CQRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public OnlineShoppingController(
            IOnlineShopRepository onlineShopRepository,
            OnlineShopDbContext onlineShopDbContext,
            SampleCpntext sampleCpntext)
        {
            _onlineShopRepository = onlineShopRepository;

            _onlineShopDbContext = onlineShopDbContext;
            _sampleCpntext = sampleCpntext;
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
            return "value";
        }

        // POST api/<OnlineShoppingController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            try
            {
               
                //var contact=new Contact()
                //{
                //    ContactId=value.ContactId,
                //    FirstName=value.FirstName,
                //    LastName=value.LastName
                //};
                _sampleCpntext.Contact.Add(value);
                _sampleCpntext.SaveChanges();
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
