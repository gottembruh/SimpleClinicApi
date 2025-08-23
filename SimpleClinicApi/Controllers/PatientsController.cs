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
   public Task<IEnumerable<PatientDto>> Get() =>
      mediator.Send(new Query.GetPatientsQuery());

   [AllowAnonymous]
   [HttpGet("{id:guid}")]
   public async Task<PatientDto?> Get(Guid id) =>
      await mediator.Send(new Query.PatientWithAllDetailsQuery(id));

   [HttpPost]
   public Task<Guid> Create(CreateUpdatePatientDto dto) =>
      mediator.Send(new CreatePatientCommand(dto));

   [HttpPut("{id:guid}")]
   public Task Update(Guid id, CreateUpdatePatientDto dto) =>
      mediator.Send(new UpdatePatientCommand(id, dto));

   [HttpDelete("{id:guid}")]
   public Task Delete(Guid id) =>
      mediator.Send(new DeletePatientCommand(id));
}