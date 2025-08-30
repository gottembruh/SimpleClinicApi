using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleClinicApi.DataAccess;

namespace SimpleClinicApi.Infrastructure;

public class DbContextTransactionPipelineBehavior<TRequest, TResponse>(ClinicDbContext context)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        TResponse? result;

        try
        {
            context.BeginTransaction();

            result = await next(cancellationToken);

            context.CommitTransaction();
        }
        catch (Exception)
        {
            context.RollbackTransaction();

            throw;
        }

        return result;
    }
}
