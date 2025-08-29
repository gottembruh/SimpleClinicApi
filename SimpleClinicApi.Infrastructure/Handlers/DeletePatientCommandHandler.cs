using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Errors;

namespace SimpleClinicApi.Infrastructure.Handlers
{
   public class DeletePatientCommandHandler(IPatientRepository repository) : IRequestHandler<DeletePatientCommand>
   {
      public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
      {
         var patient = await repository.GetByIdWholeAsync(request.Id, cancellationToken);

         if (patient == null)
         {
            throw new RestException(HttpStatusCode.NotFound, $"Patient with id {request.Id} not found.");
         }

         repository.Remove(patient);

         await repository.SaveChangesAsync(cancellationToken);
      }
   }
}