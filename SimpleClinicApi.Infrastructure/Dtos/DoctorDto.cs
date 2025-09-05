using System;

namespace SimpleClinicApi.Infrastructure.Dtos;

public record DoctorDto(Guid Id, string FullName, string Specialty, string PhoneNumber);
public record CreateUpdateDoctorDto(string FullName, string Specialty, string PhoneNumber);
