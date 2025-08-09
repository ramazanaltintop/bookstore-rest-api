using Bookstore.Application.Books.Create;
using Bookstore.Application.Books.Delete;
using Bookstore.Application.Books.Get;
using Bookstore.Application.Books.GetById;
using Bookstore.Application.Books.Patch;
using Bookstore.Application.Books.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Ramazan.Mediator;

namespace Bookstore.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BooksController(
    ISender sender) : Controller
{
    [Authorize]
    [HttpGet]
    [EnableRateLimiting("per-user")]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] GetBooksQuery query,
        CancellationToken cancellationToken)
    {
        var books = await sender.Send(query, cancellationToken);
        return Ok(books);
    }

    [Authorize]
    [HttpGet("{id:Guid}")]
    [EnableRateLimiting("per-user")]
    public async Task<IActionResult> GetOneBook(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateBook(
        [FromBody] CreateBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await sender.Send(command, cancellationToken);
        return Created($"api/books/{book.Id}", book);
    }

    [Authorize]
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateBook(
        [FromRoute(Name = "id")] Guid id,
        [FromBody] UpdateBookDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBookCommand(id, dto.ISBN, dto.Title, dto.Price, dto.StockQuantity);
        var book = await sender.Send(command, cancellationToken);
        return Ok(book);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteBook(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBookCommand(id);
        await sender.Send(command, cancellationToken);
        return Ok(new { Message = "The book has been successfully deleted" });
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> PatchBook(
        [FromRoute(Name = "id")] Guid id,
        [FromBody] JsonPatchDocument<PatchBookDto> patchDocument,
        CancellationToken cancellationToken)
    {
        await sender.Send(new PatchBookCommand(id, patchDocument), cancellationToken);
        return Ok(new { Message = "The book has been successfully updated" });
    }
}