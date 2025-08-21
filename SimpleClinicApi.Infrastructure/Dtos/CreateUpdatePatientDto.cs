using System;
using JetBrains.Annotations;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   [UsedImplicitly]
   public class CreateUpdatePatientDto
   {
      public string FullName
      {
         get;
         set;
      } = string.Empty;

      public DateTime DateOfBirth
      {
         get;
         set;
      }

      public string? PhoneNumber
      {
         get;
         set;
      }

      public string? Email
      {
         get;
         set;
      }

      public string Address
      {
         get;
         init;
      } = null!;
   }
}