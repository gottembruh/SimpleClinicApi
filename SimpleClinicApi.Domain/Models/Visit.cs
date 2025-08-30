using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleClinicApi.Domain.Models;

public class Visit
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public DateTime VisitDate { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    [Required]
    public Guid PatientId { get; init; }

    public Patient Patient { get; init; } = null!;

    [Required]
    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; init; } = null!;

    public ICollection<VisitProcedure> VisitProcedures { get; init; } = new List<VisitProcedure>();

    public ICollection<VisitMedication> VisitMedications { get; init; } =
        new List<VisitMedication>();

    public bool IsCompleted { get; init; }

    public IEnumerable<Guid> ProceduresIds => VisitProcedures.Select(x => x.ProcedureId);

    public IEnumerable<Guid> MedicationsIds => VisitMedications.Select(x => x.MedicationId);
}
