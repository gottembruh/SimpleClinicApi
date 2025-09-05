using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProceduresController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<ProcedureDto>> Get() => mediator.Send(new Query.GetProceduresQuery());

    [AllowAnonymous]
    [HttpGet("popularity-stats")]
    public async Task<ProcedurePopularityStatsDto> GetPopularityStats() =>
        await mediator.Send(new Query.GetProcedurePopularityDataQuery());

    [HttpGet("patients-lookup")]
    public async Task<IEnumerable<ProcedureWithPatientsDto>> GetProceduresWithPatientsLookup() =>
        await mediator.Send(new Query.GetProcedureToPatientsQuery());

    [HttpPost]
    public Task<Guid> Create(CreateUpdateProcedureDto dto) =>
        mediator.Send(new CreateProcedureCommand(dto));

    [HttpPut("{id:guid}")]
    public Task Update(Guid id, CreateUpdateProcedureDto dto) =>
        mediator.Send(new UpdateProcedureCommand(id, dto));

    [HttpDelete("{id:guid}")]
    public Task Delete(Guid id) => mediator.Send(new DeleteProcedureCommand(id));
}
