using CQRS.DesignPattern.Behavioral.Observer.Notification;
using CQRS.DesignPattern.Builder;
using CQRS.DesignPattern.Factory;
using CQRS.DesignPattern.Prototype;
using CQRS.DesignPattern.Singleton;
using CQRS.DesignPattern.Structural.Adapter;
using CQRS.DesignPattern.Structural.Decorator;
using CQRS.DesignPattern.Structural.Decorator.Live.FQCost;
using CQRS.Filters;
using CQRS.Models;
using CQRS.OOPS;
using CQRS.Resolution;
using CQRS.Resolution.Generic;
using CQRS.ServiceLife;
using CQRS.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRS.Controllers
{
    //[ClientIpCheckActionFilter]
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryController([FromKeyedServices("Platinum")] CreditCard platinum,
        INotificationFactory notificationFactory,
        IHouseBuilder houseBuilder,
        IEmployeeService employeesService,
        IAnalyticsAdapter analyticsAdapter,
        FoodDecorator foodDecorator,
        //IScopedService scopedService,
        //IScopedService scopedService1,
        //ITransientService transientService,
        //ITransientService transientService1,
        //ITransientService1 transientService11,
        //ITransientService1 transientService12,
        //ISingletonService singletonService,
        //ISingletonService singletonService1,
        //IScopedService2 scopedService2,
        //IScopedService2 scopedService21,
        ITransientService2 transientService2,
        ITransientService2 transientService21,
        IMasterUser masterUser,
        IUserService userService,
        Notifier notifier,
        IPriceService priceService,
        ISingleton singleton,
        
        Func<string, IService2> funcService2,
        IGenericService<Service1> genericService,
        [FromKeyedServices("service1")] IService service


        ) : ControllerBase
    {
        //readonly CreditCard _creditCard;

        [HttpGet("eventViewer")]
        public async Task<IActionResult> GetEventViewer(HttpRequestMessage req)
        {
            string jsonContent = await req.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<GridEvent[]>(jsonContent);
            return Ok(events);
        }
        // GET: api/<FactoryController>
        [DisableCors]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            notifier.Process("Hi product");
            userService.Name = "Milan";
            userService.Age = 32;
            userService.Print();
            masterUser.Print();
            userService.Print();
            transientService2.Print();
            transientService21.Print();
            //scopedService2.Print();
            //scopedService21.Print();
            //singletonService.Print();
            //singletonService1.Print();
            //transientService11.Print();
            //transientService12.Print();
            //transientService.Print();
            //transientService1.Print();
            //scopedService.Print();
            //scopedService1.Print();

            Brand brand = new OOPS.Version();
            brand.GetData("A");
            genericService.DoWork();
            funcService2("service22").DoWork();
            service.DoWork();
            singleton.PrintDetails("milan");
            analyticsAdapter.ProcessEmployees(employeesService.GetEmployees());
            var house=houseBuilder.WithWindows(6)
                .WithDoors(5)
                .WithGarden(true)
                .Build();
            Console.WriteLine(platinum.GetCreditLimit());
            notificationFactory.CreateNotification("email").Send("milan","priya");
            return new string[] { "value1", "value2", platinum.GetCreditLimit().ToString(),
                house.ToString(),foodDecorator.Description(),priceService.BasePrice().ToString()
                
            };
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
