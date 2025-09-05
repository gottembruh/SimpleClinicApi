using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models;

public class Doctor
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public required string FullName { get; init; }

    [MaxLength(100)] public required string Specialty { get; init; }

    [MaxLength(15)] public required string PhoneNumber { get; init; }

    public ICollection<Visit> Visits { get; init; } = new List<Visit>();
}
