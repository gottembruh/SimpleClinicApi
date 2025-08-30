using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models;

public class VisitProcedure : IEquatable<VisitProcedure>
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid VisitId { get; init; }

    public Visit Visit { get; init; } = null!;

    [Required]
    public Guid ProcedureId { get; set; }

    public Procedure Procedure { get; init; } = null!;

    [MaxLength(500)]
    public string? Notes { get; set; }

    #region Equality members

    public bool Equals(VisitProcedure? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id)
            && VisitId.Equals(other.VisitId)
            && ProcedureId.Equals(other.ProcedureId);
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

        return obj.GetType() == GetType() && Equals((VisitProcedure)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, VisitId, ProcedureId);
    }

    #endregion
}
