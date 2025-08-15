using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Visit
   {
      [Key]
      public Guid Id
      {
         get;
         init;
      }= Guid.NewGuid();

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
      public Guid PatientId
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
      public Guid DoctorId
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

      public bool IsCompleted
      {
         get;
         set;
      }
   }
}