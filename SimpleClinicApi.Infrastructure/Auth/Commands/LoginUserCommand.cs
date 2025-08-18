using MediatR;

namespace SimpleClinicApi.Infrastructure.Auth.Commands
{
   public class LoginUserCommand(LoginDto dto) : IRequest<AuthResponseDto>
   {
      public LoginDto LoginDto
      {
         get;
         set;
      } = dto;
   }
}