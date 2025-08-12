using static Bookstore.Application.Users.Create.CreateUserCommand;

namespace Bookstore.Application.Users.Create;

public sealed record CreateUserCommand(
    string Email,
    string Password,
    UserDetailDto UserDetail)
{
    public sealed record UserDetailDto(
        string FirstName,
        string LastName,
        byte? Age,
        string? Phone);
}