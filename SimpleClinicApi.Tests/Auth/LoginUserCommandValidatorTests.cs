using FluentValidation.TestHelper;
using SimpleClinicApi.Infrastructure.Auth;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Validators;

namespace SimpleClinicApi.Tests.Auth;

public class LoginUserCommandValidatorTests
{
   private readonly LoginUserCommandValidator _validator = new();

   private LoginUserCommand CreateCommand(string userName, string password)
   {
      return new LoginUserCommand(new LoginDto(UserName: userName, Password: password));
   }

   [Fact]
   public void Should_Have_Error_When_UserName_Is_Null_Or_Empty()
   {
      var command = CreateCommand(null, "Password1");
      var result = _validator.TestValidate(command);

      result.ShouldHaveValidationErrorFor(c => c.LoginDto.UserName)
            .WithErrorMessage("Username is required.");

      command = CreateCommand("", "Password1");
      result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.LoginDto.UserName);
   }

   [Fact]
   public void Should_Have_Error_When_UserName_Is_Too_Short()
   {
      var command = CreateCommand("ab", "Password1");
      var result = _validator.TestValidate(command);

      result.ShouldHaveValidationErrorFor(c => c.LoginDto.UserName)
            .WithErrorMessage("Username must be at least 3 characters long.");
   }

   [Fact]
   public void Should_Have_Error_When_Password_Is_Null_Or_Empty()
   {
      var command = CreateCommand("validuser", null);
      var result = _validator.TestValidate(command);

      result.ShouldHaveValidationErrorFor(c => c.LoginDto.Password)
            .WithErrorMessage("Password is required.");

      command = CreateCommand("validuser", "");
      result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.LoginDto.Password);
   }

   [Fact]
   public void Should_Have_Error_When_Password_Is_Too_Short()
   {
      var command = CreateCommand("validuser", "12345");
      var result = _validator.TestValidate(command);

      result.ShouldHaveValidationErrorFor(c => c.LoginDto.Password)
            .WithErrorMessage("Password must be at least 6 characters long.");
   }

   [Fact]
   public void Should_Not_Have_Error_For_Valid_Command()
   {
      var command = CreateCommand("validuser", "Password1");
      var result = _validator.TestValidate(command);
      result.ShouldNotHaveAnyValidationErrors();
   }
}