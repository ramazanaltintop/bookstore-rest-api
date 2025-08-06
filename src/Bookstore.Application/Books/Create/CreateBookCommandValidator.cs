using FluentValidation;

namespace Bookstore.Application.Books.Create;

public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title could not be empty")
            .NotNull().WithMessage("Title could not be null")
            .MinimumLength(2).WithMessage("Title must be at least 2 characters");

        RuleFor(b => b.Price)
            .NotEmpty().WithMessage("Price could not be empty")
            .NotNull().WithMessage("Price could not be null")
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .LessThanOrEqualTo(999999).WithMessage("High price");
    }
}