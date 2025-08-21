using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class VisitDto
   {
      public Guid Id
      {
         get;
         init;
      }

      public DateTime VisitDate
      {
         get;
         init;
      }

      public string? Notes
      {
         get;
         init;
      }

      public DoctorDto Doctor
      {
         get;
         init;
      } = null!;

      public ICollection<VisitProcedureDto>? VisitProcedures
      {
         get;
         init;
      }

      public ICollection<VisitMedicationDto>? VisitMedications
      {
         get;
         init;
      }

      public bool IsCompleted
      {
         get;
         init;
      }
   }
}