namespace Bookstore.Application.Users.Login;

public sealed record LoginUserCommandResponse(
    string UserId,
    string AccessToken);