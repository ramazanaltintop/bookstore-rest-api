using FluentValidation;

namespace Bookstore.Application.Users.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email address cannot be empty.")
            .NotNull().WithMessage("Email address cannot be null.")
            .EmailAddress().WithMessage("Please enter a valid email address.")
            .MaximumLength(254).WithMessage("Email address cannot exceed 256 characters.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .NotNull().WithMessage("Password cannot be null.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
    }
}
