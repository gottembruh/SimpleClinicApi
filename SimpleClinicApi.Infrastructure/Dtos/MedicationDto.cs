using System;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   public class MedicationDto
   {
      public Guid Id
      {
         get;
         init;
      } 

      public string Name
      {
         get;
         init;
      }

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