using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

      public ICollection<Visit> Visits
      {
         get;
         init;
      } = new List<Visit>();

      [NotMapped]
      public int VisitsCount => Visits?.Count ?? 0;

      [NotMapped]
      public IEnumerable<Guid>? VisitsIds => Visits?.Select(v => v.Id);
   }
}