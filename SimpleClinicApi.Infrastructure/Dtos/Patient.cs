using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class PatientDto(
      Guid Id,
      string FullName,
      DateTime DateOfBirth,
      string? PhoneNumber,
      string Address,
      IEnumerable<VisitDto>? Visits);

   [UsedImplicitly]
   public record CreateUpdatePatientDto(string FullName, DateTime DateOfBirth, string? PhoneNumber, string Address);
}