using FluentValidation;
using SimpleClinicApi.Infrastructure.Auth.Commands;

namespace SimpleClinicApi.Infrastructure.Auth.Validators
{
   public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
   {
      public LoginUserCommandValidator()
      {
         RuleFor(x => x.LoginDto.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.");

         RuleFor(x => x.LoginDto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
      }
   }
}