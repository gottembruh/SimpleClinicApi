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
public class PatientsController(IMediator mediator) : ControllerBase
{
   [HttpGet]
   public Task<IEnumerable<PatientDto>> Get(CancellationToken ct) =>
      mediator.Send(new Query.GetPatientsQuery(), ct);

   [AllowAnonymous]
   [HttpGet("{id:guid}")]
   public async Task<PatientDto?> Get(Guid id, CancellationToken ct) =>
      await mediator.Send(new Query.PatientWithAllDetailsQuery(id), ct);

   [HttpPost]
   public Task<Guid> Create([FromBody] CreateUpdatePatientDto dto, CancellationToken ct) =>
      mediator.Send(new CreatePatientCommand(dto), ct);

   [HttpPut("{id:guid}")]
   public Task Update(Guid id, [FromBody] CreateUpdatePatientDto dto, CancellationToken ct) =>
      mediator.Send(new UpdatePatientCommand(id, dto), ct);

   [HttpDelete("{id:guid}")]
   public Task Delete(Guid id, CancellationToken ct) =>
      mediator.Send(new DeletePatientCommand(id), ct);
}