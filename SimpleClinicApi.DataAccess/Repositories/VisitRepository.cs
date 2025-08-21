using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public class VisitRepository(ClinicDbContext context) : GenericRepository<Visit, ClinicDbContext>(context), IVisitRepository
   {
      // TODO: may be move from repository to extension method
      public IQueryable<Visit> GetAllWithDependencies()
      {
         return _dbSet.Include(x => x.VisitMedications)
                      .Include(x => x.VisitProcedures)
                      .AsNoTracking();
      }
   }
}