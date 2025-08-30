using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Errors;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Infrastructure.Handlers;

public class GetProcedurePopularityStatsHandler(
    IProcedureRepository procedureRepository,
    IMapper mapper
) : IRequestHandler<Query.GetProcedurePopularityDataQuery, ProcedurePopularityStatsDto>
{
    public async Task<ProcedurePopularityStatsDto> Handle(
        Query.GetProcedurePopularityDataQuery request,
        CancellationToken cancellationToken
    )
    {
        var (mostPopular, mostCount, leastPopular, leastCount) =
            await procedureRepository.GetPopularityStatsAsync(cancellationToken);

        if (mostPopular == null || leastPopular == null)
        {
            throw new RestException(HttpStatusCode.NotFound, "No procedures at database");
        }

        return new ProcedurePopularityStatsDto(
            mapper.Map<ProcedureDto>(mostPopular),
            mostCount,
            mapper.Map<ProcedureDto>(leastPopular),
            leastCount
        );
    }
}

public class GetProcedureToPatientsHandler(IProcedureRepository repository, IMapper mapper)
    : IRequestHandler<Query.GetProcedureToPatientsQuery, ILookup<ProcedureDto, PatientDto>>
{
    public async Task<ILookup<ProcedureDto, PatientDto>> Handle(
        Query.GetProcedureToPatientsQuery request,
        CancellationToken cancellationToken
    )
    {
        var lookup = await repository.GetProceduresToPatientsLookupAsync(cancellationToken);
        var mappedLookup = lookup.ToLookup(
            kvp => mapper.Map<ProcedureDto>(kvp.Key),
            mapper.Map<PatientDto>
        );

        return mappedLookup;
    }
}
