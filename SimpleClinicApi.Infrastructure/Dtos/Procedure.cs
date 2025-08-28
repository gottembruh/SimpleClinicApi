using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   public record ProcedureDto(Guid Id, string Name, string Description, decimal Cost);

   [UsedImplicitly]
   public record ProcedureToPatientsDto(ProcedureDto Procedure, IEnumerable<PatientDto>? Patients);

   [UsedImplicitly]
   public record ProcedurePopularityStatsDto(
      ProcedureDto MostPopular,
      int MostPopularCount,
      ProcedureDto LeastPopular,
      int LeastPopularCount);

   public record CreateUpdateProcedureDto(string Name, string? Description, decimal Cost);
}