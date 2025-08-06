using FluentValidation;

namespace Bookstore.Application.Books.GetById;

public sealed class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty().WithMessage("Id could not be empty")
            .NotNull().WithMessage("Id could not be null");
    }
}