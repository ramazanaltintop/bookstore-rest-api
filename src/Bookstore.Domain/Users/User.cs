namespace Bookstore.Domain.Users;

public sealed class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserDetail UserDetail { get; set; } = default!;
}