using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Utilities;
using SimpleClinicApi.Infrastructure.Errors;

namespace SimpleClinicApi.Infrastructure.Auth.Handlers
{
   public class LoginUserCommandHandler(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      IJwtTokenGenerator tokenGenerator)
      : IRequestHandler<LoginUserCommand, AuthResponseDto>
   {
      public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
      {
         var user = await userManager.FindByNameAsync(request.LoginDto.UserName!);

         if (user == null)
         {
            throw new RestException(HttpStatusCode.Unauthorized,"Invalid username or password");
         }

         var result = await signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password!, false);

         if (!result.Succeeded)
         {
            throw new RestException(HttpStatusCode.Unauthorized,"Invalid username or password");
         }

         var token = tokenGenerator.GenerateJwtToken(user);

         return new AuthResponseDto(token, user.UserName!);

      }
   }
}