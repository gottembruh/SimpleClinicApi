using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Domain.Models;
using SimpleClinicApi.Infrastructure.Dtos;
using SimpleClinicApi.Infrastructure.Errors;
using SimpleClinicApi.Infrastructure.Queries;

#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.

namespace SimpleClinicApi.Infrastructure.Handlers
{
   public class GetPatientByIdQueryHandler(IPatientRepository repository, IMapper mapper)
      : IRequestHandler<Query.PatientWithAllDetailsQuery, PatientDto>
   {
      public async Task<PatientDto> Handle(Query.PatientWithAllDetailsQuery request, CancellationToken cancellationToken)
      {
         var patient = await repository.GetByIdWholeAsync(request.Id, cancellationToken);

         return patient == null ?
            throw new RestException(HttpStatusCode.NotFound, $"Patient with id {request.Id} not found.") :
            mapper.Map<PatientDto>(patient);
      }
   }
}