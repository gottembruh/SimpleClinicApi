using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories;

public class ProcedureRepository(ClinicDbContext context)
    : GenericRepository<Procedure, ClinicDbContext>(context),
        IProcedureRepository
{
    public async Task<(Procedure MostPopular, int MostPopularCount, Procedure LeastPopular, int LeastPopularCount)?>
        GetPopularityStatsAsync(CancellationToken cancellationToken = default)
    {
        var proceduresByCount = await Context
            .VisitProcedures.GroupBy(vp => vp.ProcedureId)
            .Select(g => new { ProcedureId = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        if (proceduresByCount.Count == 0)
        {
            return null;
        }

        var proceduresDict = await Context
            .Procedures.Where(p => proceduresByCount.Select(pc => pc.ProcedureId).Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        var maxGroup = proceduresByCount.OrderByDescending(pc => pc.Count).First();
        var minGroup = proceduresByCount.OrderBy(pc => pc.Count).First();

        var mostPopular = proceduresDict[maxGroup.ProcedureId];
        var leastPopular = proceduresDict[minGroup.ProcedureId];

        return (mostPopular, maxGroup.Count, leastPopular, minGroup.Count);
    }

    public async Task<ILookup<Procedure, Patient>> GetProceduresToPatientsLookupAsync(
        CancellationToken cancellationToken = default
    )
    {
        var visitProcedures = await Context
            .VisitProcedures.Include(vp => vp.Procedure)
            .Include(vp => vp.Visit)
            .ThenInclude(v => v.Patient)
            .ToListAsync(cancellationToken);

        var lookup = visitProcedures.ToLookup(vp => vp.Procedure, vp => vp.Visit.Patient);

        return lookup;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        Set.AnyAsync(p => p.Id == id, cancellationToken);
}
