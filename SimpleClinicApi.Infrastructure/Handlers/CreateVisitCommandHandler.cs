using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Domain.Models;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Errors;

namespace SimpleClinicApi.Infrastructure.Handlers;

public class CreateVisitCommandHandler(
   IVisitRepository visitRepository,
   IDoctorRepository doctorRepository,
   IProcedureRepository procedureRepository,
   IMedicationRepository medicationRepository,
   IMapper mapper)
   : IRequestHandler<CreateVisitCommand, VisitDto>
{
   public async Task<VisitDto> Handle(CreateVisitCommand request, CancellationToken cancellationToken)
   {
      if (!await doctorRepository.ExistsAsync(request.Dto.DoctorId, cancellationToken))
      {
         throw new RestException(HttpStatusCode.BadRequest, new
         {
            Doctor = $"Doctor {request.Dto.DoctorId} not found"
         });
      }

      foreach (var proc in request.Dto.VisitProcedures ?? [])
      {
         if (!await procedureRepository.ExistsAsync(proc.ProcedureId, cancellationToken))
         {
            throw new RestException(HttpStatusCode.BadRequest, new
            {
               Procedure = $"Procedure {proc.ProcedureId} not found"
            });
         }
      }

      foreach (var med in request.Dto.VisitMedications ?? [])
      {
         if (!await medicationRepository.ExistsAsync(med.MedicationId, cancellationToken))
         {
            throw new RestException(HttpStatusCode.BadRequest, new
            {
               Medication = $"Medication {med.MedicationId} not found"
            });
         }
      }

      var visit = new Visit
      {
         Id = Guid.NewGuid(),
         PatientId = request.Dto.PatientId,
         DoctorId = request.Dto.DoctorId,
         VisitDate = request.Dto.VisitDate,
         Notes = request.Dto.Notes,
         VisitProcedures = mapper.Map<ICollection<VisitProcedure>>(request.Dto.VisitProcedures),
         VisitMedications = mapper.Map<ICollection<VisitMedication>>(request.Dto.VisitMedications),
         IsCompleted = false
      };

      await visitRepository.AddAsync(visit, cancellationToken);

      if (visit.VisitProcedures.Count != 0 )
      {
         await visitRepository.AddVisitProceduresRangeAsync(visit.VisitProcedures, cancellationToken);
      }

      if (visit.VisitMedications.Count != 0)
      {
         await visitRepository.AddVisitMedicationsRangeAsync(visit.VisitMedications, cancellationToken);
      }

      await visitRepository.SaveChangesAsync(cancellationToken);

      return mapper.Map<VisitDto>(visit);
   }
}