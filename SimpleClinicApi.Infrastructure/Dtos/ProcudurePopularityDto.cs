using JetBrains.Annotations;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class ProcudurePopularityDto
   {
      public Procedure MostPopular
      {
         get;
         set;
      }

      public int MostPopularCount
      {
         get;
         set;
      }

      public Procedure LeastPopular
      {
         get;
         set;
      }

      public int LeastPopularCount
      {
         get;
         set;
      }
   }
}