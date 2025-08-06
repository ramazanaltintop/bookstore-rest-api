namespace Bookstore.Domain.Abstractions;

public abstract class Entity
{
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string CreatedByFullName { get; set; } = default!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedByUserId { get; set; }
    public string? UpdatedByFullName { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedByUserId { get; set; }
    public string? DeletedByFullName { get; set; }
}