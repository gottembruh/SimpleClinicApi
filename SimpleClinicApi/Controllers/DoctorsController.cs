using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DoctorsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<DoctorDto>> Get(CancellationToken ct) =>
        mediator.Send(new Query.GetDoctorsQuery(), ct);

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<DoctorDto?> Get(Guid id, CancellationToken ct) =>
        await mediator.Send(new Query.DoctorWithAllDetailsQuery(id), ct);

    [HttpPost]
    public Task<Guid> Create([FromBody] CreateUpdateDoctorDto dto, CancellationToken ct) =>
        mediator.Send(new CreateDoctorCommand(dto), ct);

    [HttpPut("{id:guid}")]
    public Task Update(Guid id, [FromBody] CreateUpdateDoctorDto dto, CancellationToken ct) =>
        mediator.Send(new UpdateDoctorCommand(id, dto), ct);

    [HttpDelete("{id:guid}")]
    public Task Delete(Guid id, CancellationToken ct) =>
        mediator.Send(new DeleteDoctorCommand(id), ct);
}
