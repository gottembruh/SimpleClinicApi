using JetBrains.Annotations;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class CreateUpdateProcedureDto
   {
      public string Name
      {
         get;
         init;
      } = null!;

      public string? Description
      {
         get;
         init;
      }

      public decimal Cost
      {
         get;
         init;
      }
   }
}