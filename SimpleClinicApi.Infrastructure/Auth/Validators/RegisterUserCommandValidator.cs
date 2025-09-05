using FluentValidation;
using SimpleClinicApi.Infrastructure.Auth.Commands;

namespace SimpleClinicApi.Infrastructure.Auth.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.RegisterDto.UserName)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long.");

        RuleFor(x => x.RegisterDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(x => x.RegisterDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character.");
    }
}
