namespace Bookstore.Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid? UserId { get; }
    string? FullName { get; }
}
