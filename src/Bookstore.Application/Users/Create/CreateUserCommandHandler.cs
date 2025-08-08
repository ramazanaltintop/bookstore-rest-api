using Bookstore.Application.Abstractions.Authentication;
using Bookstore.Application.Abstractions.Data;
using Bookstore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Ramazan.Mediator;

namespace Bookstore.Application.Users.Create;

public sealed class CreateUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher) : ICommandHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            throw new InvalidOperationException("E-mail address already exists in the system");
        }
        var user = new User()
        {
            Id = Guid.CreateVersion7(),
            Email = command.Email,
            PasswordHash = passwordHasher.Hash(command.Password),
            UserDetail = new()
            {
                Id = Guid.CreateVersion7(),
                FirstName = command.UserDetail.FirstName,
                LastName = command.UserDetail.LastName,
                Age = command.UserDetail.Age,
                Phone = command.UserDetail.Phone
            }
        };

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}
