using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class VisitMedication
   {
      [Key]
      public int Id
      {
         get;
         set;
      }

      [Required]
      public int VisitId
      {
         get;
         set;
      }

      public Visit Visit
      {
         get;
         set;
      }

      [Required]
      public int MedicationId
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
      } // Дозировка

      [MaxLength(500)]
      public string Notes
      {
         get;
         set;
      } // Дополнительная информация
   }
}