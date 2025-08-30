using FluentValidation.TestHelper;
using SimpleClinicApi.Infrastructure.Auth;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Dtos;
using SimpleClinicApi.Infrastructure.Auth.Validators;

namespace SimpleClinicApi.Tests.Auth;

public class RegisterUserCommandValidatorTests
{
    private readonly RegisterUserCommandValidator _validator = new();

    private RegisterUserCommand CreateCommand(string? userName, string? email, string? password) =>
        new(new RegisterDto(UserName: userName, Email: email, Password: password!));

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Null_Or_Empty()
    {
        var command = CreateCommand(null, "test@example.com", "Password1!");
        var result = _validator.TestValidate(command);

        result
            .ShouldHaveValidationErrorFor(c => c.RegisterDto.UserName)
            .WithErrorMessage("Username is required.");

        command = CreateCommand("", "test@example.com", "Password1!");
        result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.RegisterDto.UserName);
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Too_Short()
    {
        var command = CreateCommand("ab", "test@example.com", "Password1!");
        var result = _validator.TestValidate(command);

        result
            .ShouldHaveValidationErrorFor(c => c.RegisterDto.UserName)
            .WithErrorMessage("Username must be at least 3 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Null_Or_Empty()
    {
        var command = CreateCommand("testuser", null, "Password1!");
        var result = _validator.TestValidate(command);

        result
            .ShouldHaveValidationErrorFor(c => c.RegisterDto.Email)
            .WithErrorMessage("Email is required.");

        command = CreateCommand("testuser", "", "Password1!");
        result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.RegisterDto.Email);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = CreateCommand("testuser", "invalidemail", "Password1!");
        var result = _validator.TestValidate(command);

        result
            .ShouldHaveValidationErrorFor(c => c.RegisterDto.Email)
            .WithErrorMessage("Email must be a valid email address.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Null_Or_Empty()
    {
        var command = CreateCommand("testuser", "test@example.com", null);
        var result = _validator.TestValidate(command);

        result
            .ShouldHaveValidationErrorFor(c => c.RegisterDto.Password)
            .WithErrorMessage("Password is required.");

        command = CreateCommand("testuser", "test@example.com", "");
        result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.RegisterDto.Password);
    }

    [Theory]
    [InlineData("Abc1!")] // Too short
    [InlineData("password1!")] // No uppercase
    [InlineData("Password!")] // No digit
    [InlineData("Password1")] // No special char
    public void Should_Have_Error_For_Invalid_Passwords(string invalidPassword)
    {
        var command = CreateCommand("testuser", "test@example.com", invalidPassword);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.RegisterDto.Password);
    }

    [Fact]
    public void Should_Not_Have_Error_For_Valid_Command()
    {
        var command = CreateCommand("validuser", "valid@example.com", "Valid1!");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
