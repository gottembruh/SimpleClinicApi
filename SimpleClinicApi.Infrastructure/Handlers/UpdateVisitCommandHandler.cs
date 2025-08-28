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

public class UpdateVisitCommandHandler(
   IVisitRepository visitRepository,
   IDoctorRepository doctorRepository,
   IProcedureRepository procedureRepository,
   IMedicationRepository medicationRepository,
   IMapper mapper)
   : IRequestHandler<UpdateVisitCommand, VisitDto>
{
   public async Task<VisitDto> Handle(UpdateVisitCommand request, CancellationToken cancellationToken)
   {
      var visit = await visitRepository.GetVisitWithDetailsAsync(request.Id, cancellationToken);

      if (visit == null)
      {
         throw new RestException(HttpStatusCode.NotFound, new
         {
            Visit = "Visit not found"
         });
      }

      var doctorId = request.Dto.DoctorId;

      if (doctorId != visit.DoctorId)
      {
         var doctorExists = await doctorRepository.ExistsAsync(doctorId, cancellationToken);

         if (!doctorExists)
         {
            throw new RestException(HttpStatusCode.BadRequest, new
            {
               Doctor = $"Doctor {doctorId} not found"
            });
         }
      }

      foreach (var procDto in request.Dto.VisitProcedures ?? [])
      {
         var procedureExists = await procedureRepository.ExistsAsync(procDto.ProcedureId, cancellationToken);

         if (!procedureExists)
         {
            throw new RestException(HttpStatusCode.BadRequest, new
            {
               Procedure = $"Procedure {procDto.ProcedureId} not found"
            });
         }
      }

      foreach (var medDto in request.Dto.VisitMedications ?? [])
      {
         var medicationExists = await medicationRepository.ExistsAsync(medDto.MedicationId, cancellationToken);

         if (!medicationExists)
         {
            throw new RestException(HttpStatusCode.BadRequest, new
            {
               Medication = $"Medication {medDto.MedicationId} not found"
            });
         }
      }

      visit.Notes = request.Dto.Notes ?? visit.Notes;
      visit.VisitDate = request.Dto.VisitDate;
      visit.DoctorId = doctorId;

      var procedures = (request.Dto.VisitProcedures ?? [])
         .Select(dto => new VisitProcedure
         {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            ProcedureId = dto.ProcedureId,
            Notes = dto.Notes,
            VisitId = visit.Id
         });

      var medications = (request.Dto.VisitMedications ?? [])
         .Select(dto => new VisitMedication
         {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            MedicationId = dto.MedicationId,
            Dosage = dto.Dosage,
            Notes = dto.Notes,
            VisitId = visit.Id
         });

      IEnumerable<VisitProcedure> proceduresToKeep = procedures as VisitProcedure[] ?? procedures.ToArray();
      IEnumerable<VisitMedication> medicationsToKeep = medications as VisitMedication[] ?? medications.ToArray();

      visitRepository.RemoveVisitProcedures(visit, proceduresToKeep);
      visitRepository.RemoveVisitMedications(visit, medicationsToKeep);
      await visitRepository.AddOrUpdateVisitProceduresAsync(visit, proceduresToKeep, cancellationToken);
      await visitRepository.AddOrUpdateVisitMedicationsAsync(visit, medicationsToKeep, cancellationToken);

      await visitRepository.SaveChangesAsync(cancellationToken);

      var updatedVisit = await visitRepository.GetVisitWithDetailsAsync(visit.Id, cancellationToken);

      if (updatedVisit == null)
      {
         throw new RestException(HttpStatusCode.NotFound, new
         {
            Visit = "Visit not found after update"
         });
      }

      return mapper.Map<VisitDto>(updatedVisit);
   }
}