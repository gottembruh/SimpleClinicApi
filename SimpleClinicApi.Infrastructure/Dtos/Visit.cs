using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.Infrastructure.Dtos;

[UsedImplicitly]
public record VisitDto(
    Guid Id,
    DateTime VisitDate,
    string? Notes,
    DoctorDto Doctor,
    ICollection<VisitProcedureDto>? VisitProcedures,
    ICollection<VisitMedicationDto>? VisitMedications,
    bool IsCompleted
);

[UsedImplicitly]
public record CreateUpdateVisitDto(
    DateTime VisitDate,
    string? Notes,
    Guid DoctorId,
    Guid PatientId,
    IEnumerable<VisitProcedureDto>? VisitProcedures,
    IEnumerable<VisitMedicationDto>? VisitMedications
);

[UsedImplicitly]
public record VisitMedicationDto(
    Guid Id,
    Guid VisitId,
    Guid MedicationId,
    string? Notes,
    string Dosage
);

[UsedImplicitly]
public record VisitProcedureDto(Guid Id, Guid VisitId, Guid ProcedureId, string? Notes);
