using CQRS.Features.Players.Commands;
using CQRS.Features.Players.Queries;
using CQRS.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            HttpContext.Session.SetInt32("UserId", 123456);
            HttpContext.Session.SetString("UserName", "info@dotnettutorials.net");
            return await _mediator.Send(new GetAllPlayersQuery());
        }
        [HttpGet("GetPlayerById")]
        public async Task<Player> GetPlayer(int id) 
        { 
            return await _mediator.Send(new GetPlayerByIdQuery() { Id=id});
        }
        [HttpPost]
        public async Task<Player> Create(CreatePlayerCommand cmd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _mediator.Send(cmd);
                    //return RedirectToAction(nameof(Index));
                }
                return new Player();
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Unable to save changes."+ex.ToString());
                return new Player();
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
