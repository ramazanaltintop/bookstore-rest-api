using Bookstore.Application.Books.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BooksController(ISender sender) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] GetBooksQuery query,
        CancellationToken cancellationToken)
    {
        var books = await sender.Send(query, cancellationToken);
        return Ok(books);
    }
}