using Alachisoft.NCache.Licensing.DOM;
using Alachisoft.NCache.Runtime.MapReduce;
using CQRS.CircuitBreaker;
using CQRS.DesignPattern.Behavioral.Observer.Notification;
using CQRS.DesignPattern.Behavioral.Template;
using CQRS.DesignPattern.Builder;
using CQRS.DesignPattern.DisposePattern;
using CQRS.DesignPattern.Factory;
using CQRS.DesignPattern.Prototype;
using CQRS.DesignPattern.Singleton;
using CQRS.DesignPattern.Structural.Adapter;
using CQRS.DesignPattern.Structural.Decorator;
using CQRS.DesignPattern.Structural.Decorator.Exam1;
using CQRS.DesignPattern.Structural.Decorator.Live.FQCost;
using CQRS.Filters;
using CQRS.Models;
using CQRS.OOPS;
using CQRS.Resolution;
using CQRS.Resolution.Generic;
using CQRS.Security;
using CQRS.ServiceLife;
using CQRS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRS.Controllers
{
    //[ClientIpCheckActionFilter]
    //[EnableCors("MyPolicy")]
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
        IPlayersService playersService,
        ITransientService2 transientService21,
        IMasterUser masterUser,
        IUserService userService,
        Notifier notifier,
        IPriceService priceService,
        ISingleton singleton,
        IExternalService externalService,
        Func<string, IService2> funcService2,
        IGenericService<Service1> genericService,
        IHouseTemplate houseTemplate,
        [FromKeyedServices("service1")] IService service


        ) : ControllerBase
    {
        [HttpGet("circuitbreaker")]
        public async Task<ActionResult<string>> getdata()
        {
            //IExternalService externalService1 = new ExternalService();
            //await externalService1.GetDataAsync();
             return await externalService.GetDataAsync();
        }
        [HttpGet("template")]
        public ActionResult GetHouse()
        {
            //IHouseTemplate houseTemplate1 = (IHouseTemplate)new ConcreteHouse();
            houseTemplate.BuildHouse();
            return Ok();
        }
        //readonly CreditCard _creditCard;
        [HttpGet("milan1234")]
        public ActionResult<IEnumerable<CountryName>> GetCompanies(string s)
        {
            var a = 0;
            playersService.GetPlayersList();
            var list = CountryName.countryNames.Where(x => x.Name.StartsWith(s)).ToList();
            //try
            //{
            //    var c = 2 / a;
            //}
            //catch (Exception)
            //{

            //}
            var c = 2 / a;
            return Ok(list);
            //return Ok(CountryName.countryNames.Where(x => x.Name.Contains(s)));
        }
        [HttpGet("eventViewer")]
        public async Task<IActionResult> GetEventViewer(HttpRequestMessage req)
        {
            string jsonContent = await req?.Content?.ReadAsStringAsync()!;
            var events = JsonConvert.DeserializeObject<GridEvent[]>(jsonContent);
            return Ok(events);
        }
        [HttpGet("GetNegotiationFile")]
        public IActionResult GetNegotiationFile()
        {
            List<string> add = new List<string>();
            add.Add("susanta0");
            add.Add("susanta1");
            add.Add("susanta2");
            add.Add("susanta3");

            return Ok(add);
        }
        [Consumes("application/xml", "application/json", "text/plain")]
        [Produces("application/xml", "application/json", "text/plain")]
        [HttpGet("GetNegotiationFile1")]
        public IActionResult GetNegotiationFile1()
        {
            List<string> add = new List<string>();
            add.Add("susanta0");
            add.Add("susanta1");
            add.Add("susanta2");
            add.Add("susanta3");

            return Ok(add);
        }
        // GET: api/<FactoryController>
        [DisableCors]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            DisposePatternTest.MainMethod();
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
        [HttpGet("category-brand")]
        public IActionResult GetProductsByCategoryAndBrandViaHeaders([FromHeader] string category, [FromHeader] string brand)
        {
            return Ok(category + brand);
        }
        [HttpGet("commonModel")]
        public IActionResult GetCoomonModel()
        {
            var aa=  Request.HttpContext.Request.Headers["city"];
            var context = Request.HttpContext.Request.Headers.ToList();
            return Ok();
        }
        [HttpPost("multiobjs")]
        public IActionResult MultipleObjects(object[] data)
        {
            CommentModel emp = JsonConvert.DeserializeObject<CommentModel>(data[0].ToString()!)!;
            CommentDetail cust = JsonConvert.DeserializeObject<CommentDetail>(data[1].ToString()!)!;
            List<CommentModel> list = new List<CommentModel>()
            {
                new CommentModel(){Content="a"},
                new CommentModel(){Content="b"},
                new CommentModel(){Content="c"},
                new CommentModel(){Content="d"}
            };
            var output = JsonConvert.SerializeObject(list);
            return Ok(data);
        }
        [HttpPost("multiobjs1")]
        public IActionResult MultipleObjects1(string data)
        {            
            var output = JsonConvert.DeserializeObject<List<CommentModel>>(data);
            return Ok(data);
        }
        [HttpPost("multiobjs2")]
        public IActionResult MultipleObjects2(object data)
        {
            var output = JsonConvert.DeserializeObject<List<CommentModel>>(data.ToString()!);
            return Ok(data);
        }
        [HttpPost("multiobjs3")]
        public IActionResult MultipleObjects3(dynamic data)
        {
            var output = JsonConvert.DeserializeObject<List<CommentModel>>(data.ToString()!);
            return Ok(output);
        }
        [HttpPost("multiobjs4")]
        public IActionResult MultipleObjects3(List<CommentModel> data)
        {
            //var output = JsonConvert.DeserializeObject<List<CommentModel>>(data.ToString()!);
            return Ok(data);
        }
        //TypeError: Failed to execute 'fetch' on 'Window': Request with GET/HEAD method cannot have body.
        [HttpGet("category")]
        public IActionResult GetProductsByCategory([FromBody] Models.Product model)//wrong
        {
            return Ok(model);
        }

        // GET api/<FactoryController>/5
        //[Authorize(Policy = "BlockGet")]
        //[EnableCors("VerbPolicy")]
        [VerbAuthorizationFilter]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            TestInheritance.MainMethod();
            string title = "<script>Something Nasty</script>";
            //Microsoft.Security.Application.AntiXssEncoder.HtmlEncode(title, true);
            var abc = HtmlSanitizer.Sanitize(title);
            abc = HtmlEncoder.Default.Encode(abc);
            return "value,"+ abc;
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
