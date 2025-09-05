using AutoMapper;
using JetBrains.Annotations;
using SimpleClinicApi.Domain.Models;
using SimpleClinicApi.Infrastructure.Dtos;

namespace SimpleClinicApi.Infrastructure;

[UsedImplicitly]
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Doctor, DoctorDto>(MemberList.None);
        CreateMap<Procedure, ProcedureDto>(MemberList.None);
        CreateMap<Medication, MedicationDto>(MemberList.None);
        CreateMap<VisitMedication, VisitMedicationDto>(MemberList.Destination);
        CreateMap<VisitProcedure, VisitProcedureDto>(MemberList.Destination);

        CreateMap<Visit, VisitDto>(MemberList.Destination);
        CreateMap<Patient, PatientDto>(MemberList.Destination).ReverseMap();
    }
}
