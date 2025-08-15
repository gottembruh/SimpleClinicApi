using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models
{
   public class Patient
   {
      [Key]
      public Guid Id
      {
         get;
         set;
      } = Guid.NewGuid();

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