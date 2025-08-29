using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public interface IPatientRepository : IRepository<Patient>
   {
      public Task<IEnumerable<Patient>> GetAllWithVisitsAsync(CancellationToken cancellationToken = default);
      public Task<Patient?> GetByIdWholeAsync(Guid id, CancellationToken cancellationToken = default);
   }

   public interface IDoctorRepository : IRepository<Doctor>
   {
      public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
   }

   public interface IProcedureRepository : IRepository<Procedure>
   {
      public Task<(Procedure MostPopular, int MostPopularCount, Procedure LeastPopular, int LeastPopularCount)>
         GetPopularityStatsAsync(CancellationToken cancellationToken = default);

      public Task<ILookup<Procedure, Patient>> GetProceduresToPatientsLookupAsync(
         CancellationToken cancellationToken = default);

      public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
   }

   public interface IMedicationRepository : IRepository<Medication>
   {
      public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
   }

   public class DoctorRepository(ClinicDbContext context)
      : GenericRepository<Doctor, ClinicDbContext>(context), IDoctorRepository
   {
      public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
      {
         return _dbSet.AnyAsync(d => d.Id == id, cancellationToken);
      }
   }

   public class MedicationRepository(ClinicDbContext context)
      : GenericRepository<Medication, ClinicDbContext>(context), IMedicationRepository
   {
      public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
      {
         return _dbSet.AnyAsync(d => d.Id == id, cancellationToken);
      }
   }
}