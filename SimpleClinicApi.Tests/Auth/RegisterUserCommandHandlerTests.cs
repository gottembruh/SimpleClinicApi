using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using SimpleClinicApi.Infrastructure.Auth;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Dtos;
using SimpleClinicApi.Infrastructure.Auth.Handlers;
using SimpleClinicApi.Infrastructure.Auth.Utilities;
using SimpleClinicApi.Infrastructure.Errors;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

namespace SimpleClinicApi.Tests.Auth;

public class RegisterUserCommandHandlerTests
{
   [Fact]
   public async Task Handle_ShouldReturnAuthResponseDto_WhenRegistrationSuccessful()
   {
      var user = new IdentityUser
      {
         UserName = "testuser",
         Email = "test@mail.com"
      };

      var userManagerMock = new Mock<UserManager<IdentityUser>>(
                                                                Mock.Of<IUserStore<IdentityUser>>(), null, null, null,
                                                                null, null, null, null, null
                                                               );

      userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                     .ReturnsAsync(IdentityResult.Success);

      var mapperMock = new Mock<IMapper>();

      mapperMock.Setup(m => m.Map<IdentityUser>(It.IsAny<RegisterDto>()))
                .Returns(user);

      var tokenGeneratorMock = new Mock<IJwtTokenGenerator>();
      tokenGeneratorMock.Setup(t => t.GenerateJwtToken(user)).Returns("testtoken");

      var handler = new RegisterUserCommandHandler(
                                                   userManagerMock.Object,
                                                   tokenGeneratorMock.Object,
                                                   mapperMock.Object);

      var command = new RegisterUserCommand(new RegisterDto("testuser", "test@mail.com", "Qq!12345"));

      var result = await handler.Handle(command, CancellationToken.None);

      result.Token.Should().Be("testtoken");
      result.UserName.Should().Be("testuser");
   }

   [Fact]
   public async Task Handle_ShouldThrowRestException_WhenRegistrationFails()
   {
      var identityErrors = new[]
      {
         new IdentityError
         {
            Code = "DuplicateUserName",
            Description = "Username already exists"
         }
      };

      var failedResult = IdentityResult.Failed(identityErrors);

      var userManagerMock = new Mock<UserManager<IdentityUser>>(
                                                                Mock.Of<IUserStore<IdentityUser>>(), null, null, null,
                                                                null, null, null, null, null);

      userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                     .ReturnsAsync(failedResult);

      var mapperMock = new Mock<IMapper>();

      mapperMock.Setup(m => m.Map<IdentityUser>(It.IsAny<RegisterDto>()))
                .Returns(new IdentityUser());

      var tokenGeneratorMock = new Mock<IJwtTokenGenerator>();

      var handler = new RegisterUserCommandHandler(userManagerMock.Object, tokenGeneratorMock.Object, mapperMock.Object);

      var command =
         new RegisterUserCommand(new RegisterDto(UserName: "testuser", Email: "test@mail.com", Password: "Qq!12345"));

      var action = () => handler.Handle(command, CancellationToken.None);

      var ex = await Assert.ThrowsAsync<RestException>(action);
      Assert.Contains("DuplicateUserName", ex.Message);
      Assert.Contains("Username already exists", ex.Message);
   }
}