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

namespace SimpleClinicApi.Infrastructure.Handlers;

public class CreateDoctorCommandHandler(IDoctorRepository repository, IMapper mapper)
    : IRequestHandler<CreateDoctorCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateDoctorCommand request,
        CancellationToken cancellationToken
    )
    {
        var doctor = mapper.Map<Doctor>(request.Doctor);

        try
        {
            await repository.AddAsync(doctor, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return doctor.Id;
        }
        catch (InvalidOperationException e)
        {
            throw new RestException(HttpStatusCode.Conflict, e.Message);
        }
    }
}
