using JetBrains.Annotations;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class ProcedurePopularityStatsDto
   {
      public ProcedureDto MostPopular
      {
         get;
         set;
      } = null!;

      public int MostPopularCount
      {
         get;
         set;
      }

      public ProcedureDto LeastPopular
      {
         get;
         set;
      } = null!;

      public int LeastPopularCount
      {
         get;
         set;
      }
   }
}