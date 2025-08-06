using Bookstore.Domain.Abstractions;

namespace Bookstore.Domain.Books;

public sealed class Book : Entity
{
    public Guid Id { get; set; }
    public string ISBN { get; set; } = default!;
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}