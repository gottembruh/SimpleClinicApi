using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Visit
   {
      [Key]
      public int Id
      {
         get;
         init;
      }

      [Required]
      public DateTime VisitDate
      {
         get;
         init;
      }

      [MaxLength(500)]
      public string? Notes
      {
         get;
         init;
      }

      [Required]
      public int PatientId
      {
         get;
         init;
      }

      public Patient Patient
      {
         get;
         init;
      } = null!;

      [Required]
      public int DoctorId
      {
         get;
         init;
      }

      public Doctor Doctor
      {
         get;
         init;
      } = null!;

      public ICollection<VisitProcedure>? VisitProcedures
      {
         get;
         init;
      }

      public ICollection<VisitMedication>? VisitMedications
      {
         get;
         init;
      }
   }
}