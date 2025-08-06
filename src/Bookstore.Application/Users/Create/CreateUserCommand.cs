using Ramazan.Mediator;

namespace Bookstore.Application.Users.Create;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand;