using MediatR;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SimpleClinicApi.Infrastructure.Dtos;

namespace SimpleClinicApi.Infrastructure.Commands
{
   [UsedImplicitly]
   public record CreatePatientCommand(CreateUpdatePatientDto Patient) : IRequest<Guid>;

   [UsedImplicitly]
   public record UpdatePatientCommand(Guid Id, CreateUpdatePatientDto Patient) : IRequest;

   [UsedImplicitly]
   public record DeletePatientCommand(Guid Id) : IRequest;
}