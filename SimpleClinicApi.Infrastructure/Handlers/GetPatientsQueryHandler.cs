using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Domain.Models;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Queries;

namespace SimpleClinicApi.Infrastructure.Handlers;

public class GetPatientsQueryHandler(IPatientRepository repository, IMapper mapper)
    : IRequestHandler<Query.GetPatientsQuery, IEnumerable<PatientDto>>
{
    public async Task<IEnumerable<PatientDto>> Handle(
        Query.GetPatientsQuery request,
        CancellationToken cancellationToken
    )
    {
        var patients = await repository.GetAllWithoutNavPropsAsync(cancellationToken);

        return mapper.Map<IEnumerable<PatientDto>>(patients);
    }
}
