namespace Bookstore.Domain.Books;

public sealed class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
}
