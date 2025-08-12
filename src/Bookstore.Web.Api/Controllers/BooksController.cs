using Bookstore.Application.Books.Create;
using Bookstore.Application.Books.Delete;
using Bookstore.Application.Books.Get;
using Bookstore.Application.Books.GetById;
using Bookstore.Application.Books.Update;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Bookstore.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BooksController : Controller
{
    [Authorize]
    [HttpGet]
    [EnableRateLimiting("per-user")]
    public async Task<IActionResult> GetAllBooks(
        [FromQuery] GetBooksQuery query,
        [FromServices] IGetBooksQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var books = await handler.HandleAsync(query, cancellationToken);
        return Ok(books);
    }

    [Authorize]
    [HttpGet("{id:Guid}")]
    [EnableRateLimiting("per-user")]
    public async Task<IActionResult> GetOneBook(
        [FromRoute(Name = "id")] Guid id,
        [FromServices] IGetBookByIdQueryHandler handler,
        [FromServices] IValidator<GetBookByIdQuery> validator,
        CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(id);
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        var result = await handler.HandleAsync(query, cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateBook(
        [FromBody] CreateBookCommand command,
        [FromServices] ICreateBookCommandHandler handler,
        [FromServices] IValidator<CreateBookCommand> validator,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        var book = await handler.HandleAsync(command, cancellationToken);
        return Created($"api/books/{book.Id}", book);
    }

    [Authorize]
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateBook(
        [FromRoute(Name = "id")] Guid id,
        [FromBody] UpdateBookDto dto,
        [FromServices] IUpdateBookCommandHandler handler,
        [FromServices] IValidator<UpdateBookCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBookCommand(id, dto.ISBN, dto.Title, dto.Price, dto.StockQuantity);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        var book = await handler.HandleAsync(command, cancellationToken);
        return Ok(book);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteBook(
        [FromRoute(Name = "id")] Guid id,
        [FromServices] IDeleteBookCommandHandler handler,
        [FromServices] IValidator<DeleteBookCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBookCommand(id);
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        await handler.HandleAsync(command, cancellationToken);
        return Ok(new { Message = "The book has been successfully deleted" });
    }
}