using CQRS.Features.Players.Commands;
using CQRS.Features.Players.Queries;
using CQRS.Models;
using CQRS.NotificationSystem;
using CQRS.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPlayerService _playerService;
        private readonly IPublisher _publisher;
        public PlayerController(IMediator mediator, IPublisher publisher, IPlayerService playerService)
        {
            _mediator = mediator;
            _playerService = playerService;
            _publisher = publisher;
        }
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            //HttpContext.Session.SetInt32("UserId", 123456);
            //HttpContext.Session.SetString("UserName", "info@dotnettutorials.net");
            
            return await _mediator.Send(new GetAllPlayersQuery());
        }
        [HttpGet("students")]
        public List<Student> GetStudents()
        {
            return _playerService.GetStudents().ToList<Student>();
        }
        [HttpGet("genericPlayer")]
        public Player GetGPlayer([FromHeader(Name ="customHeader")] Player player)
        {
            return player;
        }
        [HttpGet("products")]
        public List<Product> GetProduct()
        {
            return _playerService.GetProducts().ToList<Product>();
        }
        [HttpGet("GetPlayerById")]
        public async Task<Player> GetPlayer(int id) 
        { 
            return await _mediator.Send(new GetPlayerByIdQuery() { Id=id});
        }
        [HttpGet("GetPlayerByPage")]
        public async Task<IEnumerable<Player>> GetPlayer(int pageIndex,int pageSize)
        {
            var list= await _mediator.Send(new GetPlayersByPageQuery() { PageIndex = pageIndex, PageSize = pageSize }).ConfigureAwait(false);
            return list;
            //return await _mediator.Send(new GetPlayersByPageQuery() { PageIndex = pageIndex,PageSize= pageSize }).ConfigureAwait(false);

        }
        [HttpPost("xml_json")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        public async Task<PostPlayer> PostPlayer([FromBody] PostPlayer player)
        {
            await Task.CompletedTask;
            return player;
        }
        [HttpPost]
        public async Task<Player> Create(CreatePlayerCommand cmd)
        {
            //var partitioner=new Partitioner(cmd);
            try
            {
                if (ModelState.IsValid)
                {
                    var player=await _mediator.Send(cmd);
                    
                    await this._publisher.Publish
                        (new PlayerCreatedEvent(player.Id, player.Name));
                    return player;
                    //return await _mediator.Send(cmd);
                    //return RedirectToAction(nameof(Index));
                }
                return new Player();
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Unable to save changes."+ex.ToString());
                return new Player();
            }
        }
        [HttpPost("bulk")]
        public async Task<List<Player>> Create(List<Player> players)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _playerService.CreateBulkTablePlayers(players);
                    //return await _playerService.CreatePlayers(players);
                    //return await _mediator.Send(cmd);
                    //return RedirectToAction(nameof(Index));
                }
                return new List<Player>();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes." + ex.ToString());
                return new List<Player>();
            }
        }
        [HttpPut]
        public async Task<ActionResult<int>> Edit(int id,UpdatePlayerCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    return await _mediator.Send(command);
                    //return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes."+ex.ToString());
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<ActionResult<int>> Delete(int id)
        {
            try
            {
                return await _mediator.Send(new DeletePlayerCommand() { Id = id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete. "+ex);
                return BadRequest(ex);
            }
            //return BadRequest();
        }
    }
}
