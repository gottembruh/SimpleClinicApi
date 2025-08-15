using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Doctor
   {
      [Key]
      public int Id
      {
         get;
         set;
      }

      [Required]
      [MaxLength(100)]
      public string FullName
      {
         get;
         init;
      } = null!;

      [MaxLength(100)]
      public string Specialty
      {
         get;
         set;
      }

      [MaxLength(15)]
      public string PhoneNumber
      {
         get;
         set;
      }

      // Навигационное свойство — визиты врача
      public ICollection<Visit> Visits
      {
         get;
         set;
      }
   }
}