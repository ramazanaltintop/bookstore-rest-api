using Microsoft.AspNetCore.JsonPatch;
using Ramazan.Mediator;

namespace Bookstore.Application.Books.Patch;

public sealed record PatchBookCommand(
    Guid Id,
    JsonPatchDocument<PatchBookDto> PatchDocument) : ICommand;

public class PatchBookDto
{
    public string? Title { get; set; }
    public decimal? Price { get; set; }
}