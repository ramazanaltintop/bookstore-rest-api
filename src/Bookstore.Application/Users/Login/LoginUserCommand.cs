using Ramazan.Mediator;

namespace Bookstore.Application.Users.Login;

public sealed record LoginUserCommand(
    string Email,
    string Password) : ICommand<LoginUserCommandResponse>;