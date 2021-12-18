
using DeviceManager.Busniess.Commands.UsersCommands;
using DeviceManager.Busniess.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeviceManager.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserDTO userCred)
        {
            var loginResult = await mediator.Send(new LoginUserCommand(userCred));

            
            string token = (string)loginResult.Result;

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerCredentials)
        {
            //  var result = await userService.RegisterUserAsync(registerCredentials);

            var result = await mediator.Send(new RegisterUserCommand(registerCredentials));

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult Authorized()
        {
            return Ok();
        }

    }
}
