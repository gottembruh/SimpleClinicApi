using MediatR;

namespace SimpleClinicApi.Infrastructure.Auth.Commands
{
   public class RegisterUserCommand(RegisterDto dto) : IRequest<AuthResponseDto>
   {
      public RegisterDto RegisterDto { get; set; } = dto;
   }
}