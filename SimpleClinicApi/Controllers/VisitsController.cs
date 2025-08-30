using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<PatientDto>> Get() => mediator.Send(new Query.GetPatientsQuery());

    [HttpGet("patients-visits")]
    public async Task<ILookup<PatientDto, VisitDto>> GetPatientsToVisitsLookup()
    {
        return await mediator.Send(new Query.GetPatientToVisitsQuery());
    }

    [HttpPost]
    public Task<VisitDto> Create([FromBody] CreateUpdateVisitDto dto) =>
        mediator.Send(new CreateVisitCommand(dto));

    [HttpPut("{id:guid}")]
    public Task Update(Guid id, [FromBody] CreateUpdateVisitDto dto) =>
        mediator.Send(new UpdateVisitCommand(id, dto));

    [HttpDelete("{id:guid}")]
    public Task Delete(Guid id) => mediator.Send(new DeletePatientCommand(id));
}
