using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediatR;
using SimpleClinicApi.Infrastructure.Dtos;

namespace SimpleClinicApi.Infrastructure.Queries;

[UsedImplicitly]
public class Query
{
    [UsedImplicitly]
    public record GetPatientsQuery : IRequest<IEnumerable<PatientDto>>;

    [UsedImplicitly]
    public record PatientWithAllDetailsQuery(Guid Id) : IRequest<PatientDto?>;

    [UsedImplicitly]
    public record GetProcedureToPatientsQuery : IRequest<IEnumerable<ProcedureWithPatientsDto>>;

    [UsedImplicitly]
    public record GetPatientToVisitsQuery : IRequest<ILookup<PatientDto, VisitDto>>;

    [UsedImplicitly]
    public record GetProcedurePopularityDataQuery : IRequest<ProcedurePopularityStatsDto>;

    [UsedImplicitly]
    public record GetVisitsQuery(int? Limit, int? Offset) : IRequest<VisitDto>;

    [UsedImplicitly]
    public record GetProceduresQuery : IRequest<IEnumerable<ProcedureDto>>;
}
