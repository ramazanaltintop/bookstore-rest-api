using Bookstore.Application.Users.Create;
using Bookstore.Application.Users.Login;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] CreateUserCommand command,
        [FromServices] ICreateUserCommandHandler handler,
        [FromServices] IValidator<CreateUserCommand> validator,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        await handler.HandleAsync(command, cancellationToken);
        return Ok(new { Message = "User has been registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command,
        [FromServices] ILoginUserCommandHandler handler,
        [FromServices] IValidator<LoginUserCommand> validator,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        var response = await handler.HandleAsync(command, cancellationToken);
        return Ok(response);
    }
}