using FluentValidation;

namespace Bookstore.Application.Books.Delete;

public sealed class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id could not be empty")
            .NotNull().WithMessage("Id could not be null");
    }
}