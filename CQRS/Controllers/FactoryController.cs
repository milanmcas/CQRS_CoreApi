using CQRS.DesignPattern.Builder;
using CQRS.DesignPattern.Factory;
using CQRS.DesignPattern.Prototype;
using CQRS.DesignPattern.Structural.Adapter;
using CQRS.DesignPattern.Structural.Decorator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController([FromKeyedServices("Platinum")] CreditCard platinum,
        INotificationFactory notificationFactory,
        IHouseBuilder houseBuilder,
        IEmployeeService employeesService,
        IAnalyticsAdapter analyticsAdapter,
        FoodDecorator foodDecorator
        ) : ControllerBase
    {
        //readonly CreditCard _creditCard;

        
        // GET: api/<FactoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            analyticsAdapter.ProcessEmployees(employeesService.GetEmployees());
            var house=houseBuilder.WithWindows(6)
                .WithDoors(5)
                .WithGarden(true)
                .Build();
            Console.WriteLine(platinum.GetCreditLimit());
            notificationFactory.CreateNotification("email").Send("milan","priya");
            return new string[] { "value1", "value2", platinum.GetCreditLimit().ToString(), 
                house.ToString(),foodDecorator.Description() };
        }

        // GET api/<FactoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FactoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FactoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FactoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
