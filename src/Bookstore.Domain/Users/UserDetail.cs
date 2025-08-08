namespace Bookstore.Domain.Users;

public sealed class UserDetail
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public byte? Age { get; set; }
    public string? Phone { get; set; }
    public Guid UserId { get; set; }
}
