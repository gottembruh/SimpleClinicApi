using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class VisitProcedure
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
      } = null!;

      [Required]
      public Guid ProcedureId
      {
         get;
         init;
      }

      public Procedure Procedure
      {
         get;
         init;
      } = null!;

      [MaxLength(500)]
      public string? Notes
      {
         get;
         init;
      }
   }
}