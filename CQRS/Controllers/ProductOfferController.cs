﻿using CQRS.DesignPattern.CancellationPattern;
using CQRS.Extensions;
using CQRS.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CQRS.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOfferController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        public ProductOfferController(IHubContext<MessageHub, IMessageHubClient> _messageHub)
        {
            messageHub = _messageHub;
        }
        [HttpPost]
        [Route("productoffers")]
        public async ValueTask<string> Get()
        {
            List<string> offers = new List<string>();
            offers.Add("20% Off on IPhone 12");
            offers.Add("15% Off on HP Pavillion");
            offers.Add("25% Off on Samsung Smart TV");
            await messageHub.Clients.All.SendOffersToUser(offers);
            ExtensionTest.MainMethod();
            await TestCancellationTask.MainMethod();
            return "Offers sent successfully to all users!";
        }
        [HttpPost]
        [Route("offers")]
        public string Post([FromBody]List<string> offers)
        {
            
            //List<string> offers = new List<string>();
            //offers.Add("20% Off on IPhone 12");
            //offers.Add("15% Off on HP Pavillion");
            //offers.Add("25% Off on Samsung Smart TV");
            messageHub.Clients.All.SendOffersToUser(offers);
            return "Offers sent successfully to all users!";
        }
    }
}
