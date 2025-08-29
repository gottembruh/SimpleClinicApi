using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Dtos;
using SimpleClinicApi.Infrastructure.Auth.Utilities;
using SimpleClinicApi.Infrastructure.Errors;

namespace SimpleClinicApi.Infrastructure.Auth.Handlers
{
   public class RegisterUserCommandHandler(
      UserManager<IdentityUser> userManager,
      IJwtTokenGenerator tokenGenerator,
      IMapper mapper)
      : IRequestHandler<RegisterUserCommand, AuthResponseDto>
   {
      public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
      {
         var user = mapper.Map<IdentityUser>(request.RegisterDto);
         var result = await userManager.CreateAsync(user, request.RegisterDto.Password);

         if (!result.Succeeded)
         {
            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
               sb.AppendLine($"Error Code: {error.Code}, Description: {error.Description}");
            }

            throw new RestException(HttpStatusCode.Unauthorized, sb.ToString());
         }

         var token = tokenGenerator.GenerateJwtToken(user);

         return new AuthResponseDto(token, user.UserName!);

      }
   }
}