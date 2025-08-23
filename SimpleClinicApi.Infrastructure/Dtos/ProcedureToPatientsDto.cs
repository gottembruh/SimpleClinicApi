using System.Collections.Generic;
using JetBrains.Annotations;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class ProcedureToPatientsDto
   {
      public Procedure Procedure
      {
         get;
         set;
      } = null!;

      public IEnumerable<Patient>? Patients
      {
         get;
         set;
      }
   }
}