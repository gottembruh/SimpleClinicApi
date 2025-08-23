using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Domain.Models;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Errors;

namespace SimpleClinicApi.Infrastructure.Handlers
{
   public class CreatePatientCommandHandler(IPatientRepository repository, IMapper mapper)
      : IRequestHandler<CreatePatientCommand, Guid>
   {
      public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
      {
         var patient = mapper.Map<Patient>(request.Patient);

         try
         {
            await repository.AddAsync(patient, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return patient.Id;
         }
         catch (InvalidOperationException e)
         {
            throw new RestException(HttpStatusCode.Conflict, e.Message);
         }
      }
   }
}