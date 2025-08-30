using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models;

public class Medication
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(150)]
    public string Name { get; init; } = null!;

    [MaxLength(300)]
    public string? Description { get; init; }

    public decimal Cost { get; init; }
}
