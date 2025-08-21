using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class PatientDto
   {
      public Guid Id
      {
         get;
         init;
      }

      public string FullName
      {
         get;
         init;
      }

      public DateTime DateOfBirth
      {
         get;
         init;
      }

      public string? PhoneNumber
      {
         get;
         init;
      }

      public IEnumerable<VisitDto>? Visits
      {
         get;
         init;
      }

      public string Address
      {
         get;
         init;
      } = null!;
   }
}