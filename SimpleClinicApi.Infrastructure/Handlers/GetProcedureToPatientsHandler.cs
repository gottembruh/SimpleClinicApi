using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Infrastructure.Handlers;

public class GetProcedureToPatientsHandler(IProcedureRepository repository, IMapper mapper)
    : IRequestHandler<Query.GetProcedureToPatientsQuery, IEnumerable<ProcedureWithPatientsDto>>
{
    public async Task<IEnumerable<ProcedureWithPatientsDto>> Handle(
        Query.GetProcedureToPatientsQuery request,
        CancellationToken cancellationToken
    )
    {
        var lookup = await repository.GetProceduresToPatientsLookupAsync(cancellationToken);
        var list = lookup.Select(g => new ProcedureWithPatientsDto(
            Procedure: mapper.Map<ProcedureDto>(g.Key),
            Patients: g.Select(mapper.Map<PatientDto>)
        )).ToList();

        return list;
    }
}
