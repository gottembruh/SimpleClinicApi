using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Patient
   {
      [Key]
      public int Id
      {
         get;
         set;
      }

      [Required, MaxLength(100)]
      public string FullName
      {
         get;
         init;
      } = null!;

      public DateTime DateOfBirth
      {
         get;
         init;
      }

      [MaxLength(15)]
      public string PhoneNumber
      {
         get;
         init;
      } = null!;

      public string Address
      {
         get;
         init;
      } = null!;

      public ICollection<Visit>? Visits
      {
         get;
         init;
      }
   }
}