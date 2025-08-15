using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class VisitMedication
   {
      [Key]
      public Guid Id
      {
         get;
         init;
      } = Guid.NewGuid();

      [Required]
      public Guid VisitId
      {
         get;
         init;
      }

      public Visit Visit
      {
         get;
         init;
      }

      [Required]
      public Guid MedicationId
      {
         get;
         set;
      }

      public Medication Medication
      {
         get;
         set;
      }

      [MaxLength(100)]
      public string Dosage
      {
         get;
         set;
      }

      [MaxLength(500)]
      public string Notes
      {
         get;
         set;
      }
   }
}