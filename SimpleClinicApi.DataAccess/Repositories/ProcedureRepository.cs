using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public class ProcedureRepository(ClinicDbContext context)
      : GenericRepository<Procedure, ClinicDbContext>(context), IProcedureRepository
   {
      public async Task<(Procedure MostPopular, int MostPopularCount, Procedure LeastPopular, int LeastPopularCount)>
         GetPopularityStatsAsync(CancellationToken cancellationToken = default)
      {
         var procedureCounts = await _context.VisitProcedures
                                             .GroupBy(vp => vp.ProcedureId)
                                             .Select(g => new
                                             {
                                                ProcedureId = g.Key,
                                                Count = g.Count()
                                             })
                                             .ToListAsync(cancellationToken);

         if (procedureCounts.Count == 0) {}

         var proceduresDict = await _context.Procedures
                                            .Where(p => procedureCounts.Select(pc => pc.ProcedureId).Contains(p.Id))
                                            .ToDictionaryAsync(p => p.Id, cancellationToken);

         var maxGroup = procedureCounts.OrderByDescending(pc => pc.Count).First();
         var minGroup = procedureCounts.OrderBy(pc => pc.Count).First();

         var mostPopular = proceduresDict[maxGroup.ProcedureId];
         var leastPopular = proceduresDict[minGroup.ProcedureId];

         return (mostPopular, maxGroup.Count, leastPopular, minGroup.Count);
      }

      public async Task<ILookup<Procedure, Patient>> GetProceduresToPatientsLookupAsync(
         CancellationToken cancellationToken = default)
      {
         var visitProcedures = await _context.VisitProcedures
                                             .Include(vp => vp.Procedure)
                                             .Include(vp => vp.Visit)
                                             .ThenInclude(v => v.Patient)
                                             .ToListAsync(cancellationToken);

         var lookup = visitProcedures.ToLookup(vp => vp.Procedure, vp => vp.Visit.Patient);

         return lookup;
      }

      public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
      {
         return _dbSet.AnyAsync( p =>  p.Id == id , cancellationToken);
      }
   }
}