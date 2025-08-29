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
   public class UpdatePatientCommandHandler(IPatientRepository repository, IMapper mapper)
      : IRequestHandler<UpdatePatientCommand>
   {
      public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
      {
         var patient = await repository.GetByIdWholeAsync(request.Id, cancellationToken);

         if (patient == null)
         {
            throw new RestException(HttpStatusCode.NotFound, $"Patient with id {request.Id} not found.");
         }

         mapper.Map(request.Patient, patient);

         repository.Update(patient);
         await repository.SaveChangesAsync(cancellationToken);
      }
   }
}