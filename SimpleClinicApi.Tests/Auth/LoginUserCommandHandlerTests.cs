using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Dtos;
using SimpleClinicApi.Infrastructure.Auth.Handlers;
using SimpleClinicApi.Infrastructure.Auth.Utilities;
using SimpleClinicApi.Infrastructure.Errors;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

namespace SimpleClinicApi.Tests.Auth;

public class LoginUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAuthResponseDto_WhenLoginSuccessful()
    {
        var user = new IdentityUser { UserName = "testuser" };

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

        var signInManagerMock = new Mock<SignInManager<IdentityUser>>(
            userManagerMock.Object,
            Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
            null,
            null,
            null,
            null
        );

        signInManagerMock
            .Setup(s => s.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
            .ReturnsAsync(SignInResult.Success);

        var tokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        tokenGeneratorMock.Setup(t => t.GenerateJwtToken(user)).Returns("testtoken");

        var handler = new LoginUserCommandHandler(
            userManagerMock.Object,
            signInManagerMock.Object,
            tokenGeneratorMock.Object
        );

        var command = new LoginUserCommand(
            new LoginDto(UserName: "testuser", Password: "Password1")
        );

        var result = await handler.Handle(command, CancellationToken.None);

        result.Token.Should().Be("testtoken");
        result.UserName.Should().Be("testuser");
    }

    [Fact]
    public async Task Handle_ShouldThrowRestException_WhenUserNotFound()
    {
        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        userManagerMock
            .Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((IdentityUser?)null);

        var signInManagerMock = new Mock<SignInManager<IdentityUser>>(
            userManagerMock.Object,
            Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
            null,
            null,
            null,
            null
        );

        var tokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        var handler = new LoginUserCommandHandler(
            userManagerMock.Object,
            signInManagerMock.Object,
            tokenGeneratorMock.Object
        );

        var command = new LoginUserCommand(
            new LoginDto(UserName: "baduser", Password: "Password1")
        );

        var action = () => handler.Handle(command, CancellationToken.None);

        await action
            .Should()
            .ThrowAsync<RestException>()
            .WithMessage("Invalid username or password");
    }

    [Fact]
    public async Task Handle_ShouldThrowRestException_WhenPasswordCheckFails()
    {
        var user = new IdentityUser { UserName = "testuser" };

        var userManagerMock = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

        var signInManagerMock = new Mock<SignInManager<IdentityUser>>(
            userManagerMock.Object,
            Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
            null,
            null,
            null,
            null
        );

        signInManagerMock
            .Setup(s => s.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
            .ReturnsAsync(SignInResult.Failed);

        var tokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        var handler = new LoginUserCommandHandler(
            userManagerMock.Object,
            signInManagerMock.Object,
            tokenGeneratorMock.Object
        );

        var command = new LoginUserCommand(
            new LoginDto(UserName: "testuser", Password: "badpassword")
        );

        var action = () => handler.Handle(command, CancellationToken.None);

        await action
            .Should()
            .ThrowAsync<RestException>()
            .WithMessage("Invalid username or password");
    }
}
