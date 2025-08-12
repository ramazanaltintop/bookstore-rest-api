using Bookstore.Application.Abstractions.Authentication;
using Bookstore.Application.Abstractions.Data;
using Bookstore.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Users.Login;

public interface ILoginUserCommandHandler : IHandler
{
    Task<LoginUserCommandResponse> HandleAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default);
}

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider)
    : ILoginUserCommandHandler
{
    public async Task<LoginUserCommandResponse> HandleAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
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