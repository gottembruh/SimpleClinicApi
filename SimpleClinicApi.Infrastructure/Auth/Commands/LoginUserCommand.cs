using MediatR;
using SimpleClinicApi.Infrastructure.Auth.Dtos;

namespace SimpleClinicApi.Infrastructure.Auth.Commands;

public class LoginUserCommand(LoginDto dto) : IRequest<AuthResponseDto>
{
    public LoginDto LoginDto { get; set; } = dto;
}
