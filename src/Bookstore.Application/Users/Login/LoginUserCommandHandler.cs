using Bookstore.Application.Abstractions.Authentication;
using Bookstore.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Users.Login;

public sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider)
    : ICommandHandler<LoginUserCommand, LoginUserCommandResponse>
{
    public async Task<LoginUserCommandResponse> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include("UserDetail")
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken)
                ?? throw new KeyNotFoundException("Please check your login information.");

        bool isVerified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!isVerified)
            throw new KeyNotFoundException("Please check your login information.");

        string accessToken = tokenProvider.CreateAccessToken(user);

        return new(user.Id.ToString(), accessToken);
    }
}
