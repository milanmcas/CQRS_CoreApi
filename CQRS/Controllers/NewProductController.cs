using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CQRS.Data.Repository;
using CQRS.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewProductController(IProductSolrRepository productSolrRepository,IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSecret() {
            var keyVaultUri = configuration.GetSection("KeyVaultConfiguration:KeyVaultURL").Value;
            var clientId = configuration.GetSection("KeyVaultConfiguration:ClientId").Value;
            var clientSecret = configuration.GetSection("KeyVaultConfiguration:ClientSecret").Value;
            var directoryId = configuration.GetSection("KeyVaultConfiguration:DirectoryId").Value;

            var credential = new ClientSecretCredential(directoryId?.ToString(), clientId!.ToString(), clientSecret!.ToString());
            var secretClient = new SecretClient(new Uri(keyVaultUri!.ToString()), credential);

            var connectionStringSecret = secretClient.GetSecret("storagecredentials");
            var value = configuration["storagecredentials"];
            var a= "Value for Secret [storagecredentials] is : " + value;
            return Ok(new { or= connectionStringSecret.Value,next= a});
        }
        // GET: api/<NewproductController1>
        [HttpGet("{search}")]
        public async Task<IEnumerable<NewProduct>> Get(string search)
        {
            return await productSolrRepository.Search(search);
        }

        // GET api/<NewproductController1>/5
        [HttpGet("{id}")]
        public async Task<NewProduct> GetById(string id)
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Welcome to Shopping World!"));
            return await productSolrRepository.GetById(id);
        }

        // POST api/<NewproductController1>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewProduct value)
        {
            await productSolrRepository.Add(value);
            return Ok(value);
        }

        // PUT api/<NewproductController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewproductController1>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] NewProduct value)
        {
            await productSolrRepository.Delete(value);
            return Ok(value);
        }
    }
}
