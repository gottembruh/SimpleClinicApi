using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleClinicApi.Domain.Models;

public class VisitMedication
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid VisitId { get; init; }

    public Visit Visit { get; init; } = null!;

    [Required]
    public Guid MedicationId { get; set; }

    public Medication Medication { get; init; } = null!;

    [MaxLength(100)]
    public string Dosage { get; set; } = null!;

    [MaxLength(500)]
    public string? Notes { get; set; }
}
