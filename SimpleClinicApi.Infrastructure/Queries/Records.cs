using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;
using SimpleClinicApi.Infrastructure.Dtos;

namespace SimpleClinicApi.Infrastructure.Queries
{
   [UsedImplicitly]
   public class Query
   {
      [UsedImplicitly]
      public record GetPatientsQuery : IRequest<IEnumerable<PatientDto>>;

      [UsedImplicitly]
      public record PatientWithAllDetails(Guid Id) : IRequest<PatientDto?>;


      public record GetVisitsQuery(int? Limit, int? Offset) : IRequest<VisitDto>;

   }


}