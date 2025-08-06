using FluentValidation;

namespace Bookstore.Application.Books.Patch;

public sealed class PatchBookCommandValidator : AbstractValidator<PatchBookCommand>
{
    public PatchBookCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id could not be empty")
            .NotNull().WithMessage("Id could not be null");

        RuleFor(b => b.PatchDocument)
            .NotEmpty().WithMessage("Patch document could not be empty")
            .NotNull().WithMessage("Patch document could not be null")
            .Must(doc => doc.Operations.Any()).WithMessage("There must be at least 1 operation.");
    }
}