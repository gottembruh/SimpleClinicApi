using System.ComponentModel.DataAnnotations;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   public class VisitProcedureDto
   {
      public ProcedureDto Procedure
      {
         get;
         init;
      } = null!;

      public string? Notes
      {
         get;
         init;
      }
   }

   public class VisitMedicationDto
   {
      public Medication Medication
      {
         get;
         init;
      } = null!;


      public string Dosage
      {
         get;
         init;
      } = null!;

      public string? Notes
      {
         get;
         init;
      }
   }
}