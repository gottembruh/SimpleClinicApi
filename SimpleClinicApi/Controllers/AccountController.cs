using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleClinicApi.Infrastructure.Auth;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Dtos;

namespace SimpleClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<AuthResponseDto> Register(RegisterDto dto) => await mediator.Send(new RegisterUserCommand(dto));

    [HttpPost("login")]
    public async Task<AuthResponseDto> Login(LoginDto dto) => await mediator.Send(new LoginUserCommand(dto));
}
