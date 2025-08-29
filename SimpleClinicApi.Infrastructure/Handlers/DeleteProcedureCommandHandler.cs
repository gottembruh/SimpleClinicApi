using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Infrastructure.Commands;
using SimpleClinicApi.Infrastructure.Errors;

namespace SimpleClinicApi.Infrastructure.Handlers
{
   public class DeleteProcedureCommandHandler(IProcedureRepository repository) : IRequestHandler<DeleteProcedureCommand>
   {
      public async Task Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
      {
         var procedure = await repository.GetByIdWithoutNavPropsAsync(request.Id, cancellationToken);

         if (procedure == null)
         {
            throw new RestException(HttpStatusCode.NotFound, $"Procedure with id {request.Id} not found.");
         }

         repository.Remove(procedure);
         await repository.SaveChangesAsync(cancellationToken);
      }
   }
}