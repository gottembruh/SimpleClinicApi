using System;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   public class ProcedureDto
   {
      public Guid Id
      {
         get;
         init;
      } 

     
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