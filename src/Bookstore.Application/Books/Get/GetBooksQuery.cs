namespace Bookstore.Application.Books.Get;

public sealed record GetBooksQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "");