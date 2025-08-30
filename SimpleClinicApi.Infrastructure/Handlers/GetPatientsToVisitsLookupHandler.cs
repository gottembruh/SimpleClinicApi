using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Infrastructure.Handlers;

public class GetPatientsToVisitsLookupHandler(IVisitRepository visitRepository, IMapper mapper)
    : IRequestHandler<Query.GetPatientToVisitsQuery, ILookup<PatientDto, VisitDto>>
{
    public async Task<ILookup<PatientDto, VisitDto>> Handle(
        Query.GetPatientToVisitsQuery request,
        CancellationToken cancellationToken
    )
    {
        var lookup = await visitRepository.GetPatientsToVisitsLookupAsync(cancellationToken);

        return lookup.ToLookup(kvp => mapper.Map<PatientDto>(kvp.Key), mapper.Map<VisitDto>);
    }
}
