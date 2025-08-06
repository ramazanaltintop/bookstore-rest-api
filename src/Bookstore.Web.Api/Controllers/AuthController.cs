using Bookstore.Application.Users.Create;
using Bookstore.Application.Users.Login;
using Microsoft.AspNetCore.Mvc;
using Ramazan.Mediator;

namespace Bookstore.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
        return Ok(new { Message = "User has been registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(command, cancellationToken);
        return Ok(response);
    }
}