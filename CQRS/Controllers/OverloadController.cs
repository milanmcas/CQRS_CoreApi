using Azure.Core;
using CQRS.Security.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverloadController : ControllerBase
    {
        private readonly IValidator<UserRegistrationRequest> _validator;

        public OverloadController(IValidator<UserRegistrationRequest> validator)
        {
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequest userRegistration)
        {
            var validationResult = await _validator.ValidateAsync(userRegistration);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToList());
                //return BadRequest(Results.ValidationProblem(validationResult.ToDictionary()));
            }
            return Ok(userRegistration);
        }
        //[HttpGet]
        //[ActionName("get")]
        //public IActionResult get()
        //{
        //    return Ok("Value1");
        //}
        //[HttpGet]
        //[ActionName("Get With Parameter")]
        //public IActionResult get(string text)
        //{
        //    return Ok("Get With Parameter");
        //}
        [HttpGet()]
        public IActionResult Index()
        {
            return Ok("Value1");
        }
        [HttpGet]
        [Route("Home/Index/{i:int}")]
        public IActionResult Index(int i)
        {
            return Ok("Value1");
        }
        [HttpGet]
        [Route("Home/Index/{isDeleted:bool}")]
        public IActionResult Index(bool isDeleted)
        {
            return Ok("Value1");
        }
        [HttpGet("index1")]
        public IActionResult Index1()
        {
            return Ok("Value1");
        }
        [NonAction]
        public IActionResult Index1(string text)
        {
            return Ok("Value1");
        }
    }
}
