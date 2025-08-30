using System;

namespace SimpleClinicApi.Infrastructure.Dtos;

public record MedicationDto(Guid Id, string Name, string? Description, decimal Cost);
