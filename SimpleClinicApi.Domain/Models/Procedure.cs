using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Procedure
   {
      [Key]
      public int Id
      {
         get;
         set;
      }

      [Required]
      [MaxLength(150)]
      public string Name
      {
         get;
         set;
      }

      public string Description
      {
         get;
         set;
      }

      public decimal Cost
      {
         get;
         set;
      }
   }
}