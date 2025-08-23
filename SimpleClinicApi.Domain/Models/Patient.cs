using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SimpleClinicApi.Domain.Models
{
   public class Patient : IEquatable<Patient>
   {
     [Key]
      public Guid Id
      {
         get;
         init;
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

      #region Equality members

      public bool Equals(Patient? other)
      {
         if (other is null)
         {
            return false;
         }

         if (ReferenceEquals(this, other))
         {
            return true;
         }

         return FullName == other.FullName && DateOfBirth.Equals(other.DateOfBirth) && PhoneNumber == other.PhoneNumber;
      }

      public override bool Equals(object? obj)
      {
         if (obj is null)
         {
            return false;
         }

         if (ReferenceEquals(this, obj))
         {
            return true;
         }

         if (obj.GetType() != GetType())
         {
            return false;
         }

         return Equals((Patient)obj);
      }

      public override int GetHashCode()
      {
         return HashCode.Combine(FullName, DateOfBirth, PhoneNumber);
      }

      #endregion
   }
}