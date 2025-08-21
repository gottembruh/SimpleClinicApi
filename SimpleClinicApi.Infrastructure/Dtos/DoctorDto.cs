using System;

namespace SimpleClinicApi.Infrastructure.Dtos
{
   public class DoctorDto
   {
      public Guid Id
      {
         get;
         init;
      }

      public string FullName
      {
         get;
         init;
      } = null!;

      public string Specialty
      {
         get;
         set;
      }

      public string PhoneNumber
      {
         get;
         set;
      }
   }
}