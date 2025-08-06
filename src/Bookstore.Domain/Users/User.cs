namespace Bookstore.Domain.Users;

public sealed class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}