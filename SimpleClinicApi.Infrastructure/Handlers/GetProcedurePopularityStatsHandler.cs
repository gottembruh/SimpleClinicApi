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
        var groupResult = await procedureRepository.GetPopularityStatsAsync(cancellationToken) ??
                          throw new RestException(HttpStatusCode.NotFound, "No procedures at database");

        var (mostPopular, mostCount, leastPopular, leastCount) = groupResult;

        return new ProcedurePopularityStatsDto(
            mapper.Map<ProcedureDto>(mostPopular),
            mostCount,
            mapper.Map<ProcedureDto>(leastPopular),
            leastCount
        );
    }
}
