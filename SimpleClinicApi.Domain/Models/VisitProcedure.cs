using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class VisitProcedure
   {
      [Key]
      public int Id
      {
         get;
         init;
      }

      [Required]
      public int VisitId
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
      public int ProcedureId
      {
         get;
         init;
      }

      public Procedure? Procedure
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
   }
}