using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public interface IPatientRepository : IRepository<Patient>;

   public interface IDoctorRepository : IRepository<Doctor>;

   public interface IVisitRepository : IRepository<Visit>
   {
      public IQueryable<Visit> GetAllWithDependencies();
   }

   public interface IProcedureRepository : IRepository<Procedure>
   {
      public Task<(Procedure MostPopular, int MostPopularCount, Procedure LeastPopular, int LeastPopularCount)>
         GetPopularityStatsAsync(CancellationToken cancellationToken = default);

      public Task<ILookup<Procedure, Patient>> GetProceduresWithPatientsLookupAsync(
         CancellationToken cancellationToken = default);
   }

   public interface IMedicationRepository : IRepository<Medication>;

   public interface IVisitProcedureRepository : IRepository<VisitProcedure>;

   public interface IVisitMedicationRepository : IRepository<VisitMedication>;

   public class DoctorRepository(ClinicDbContext context)
      : GenericRepository<Doctor, ClinicDbContext>(context), IDoctorRepository;

   public class MedicationRepository(ClinicDbContext context)
      : GenericRepository<Medication, ClinicDbContext>(context), IMedicationRepository;

   public class VisitProcedureRepository(ClinicDbContext context)
      : GenericRepository<VisitProcedure, ClinicDbContext>(context), IVisitProcedureRepository;

   public class VisitMedicationRepository(ClinicDbContext context)
      : GenericRepository<VisitMedication, ClinicDbContext>(context), IVisitMedicationRepository;
}