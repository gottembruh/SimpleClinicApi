using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Procedure
   {
      [Key]
      public Guid Id
      {
         get;
         set;
      } = Guid.NewGuid();

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