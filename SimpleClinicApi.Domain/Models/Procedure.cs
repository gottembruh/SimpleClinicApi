using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models;

public class Procedure : IEquatable<Procedure>
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(150)]
    public required string Name { get; init; }

    public string? Description { get; init; }

    public decimal Cost { get; init; }

    #region Equality members

    public bool Equals(Procedure? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
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

        return obj.GetType() == GetType() && Equals((Procedure)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();

    #endregion
}
