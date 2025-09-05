using System;
using JetBrains.Annotations;
using MediatR;
using SimpleClinicApi.Infrastructure.Dtos;

namespace SimpleClinicApi.Infrastructure.Commands;

[UsedImplicitly]
public record CreatePatientCommand(CreateUpdatePatientDto Patient) : IRequest<Guid>;

[UsedImplicitly]
public record UpdatePatientCommand(Guid Id, CreateUpdatePatientDto Patient) : IRequest;

[UsedImplicitly]
public record DeletePatientCommand(Guid Id) : IRequest;

[UsedImplicitly]
public record CreateProcedureCommand(CreateUpdateProcedureDto Procedure) : IRequest<Guid>;

[UsedImplicitly]
public record UpdateProcedureCommand(Guid Id, CreateUpdateProcedureDto Procedure) : IRequest;

[UsedImplicitly]
public record DeleteProcedureCommand(Guid Id) : IRequest;

[UsedImplicitly]
public record CreateVisitCommand(CreateUpdateVisitDto Dto) : IRequest<VisitDto>;

[UsedImplicitly]
public record UpdateVisitCommand(Guid Id, CreateUpdateVisitDto Dto) : IRequest<VisitDto>;

[UsedImplicitly]
public record DeleteVisitCommand(Guid Id) : IRequest;
