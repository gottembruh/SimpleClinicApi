using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public class PatientRepository(ClinicDbContext context)
      : GenericRepository<Patient, ClinicDbContext>(context), IPatientRepository
   {
      public override async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken = default)
      {
         return await _dbSet.Include(x => x.Visits)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken: cancellationToken);
      }



      public override async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
      {
         return await _context.Patients
                              .Include(p => p.Visits)
                              .ThenInclude(v => v.Doctor).Include(p => p.Visits)
                              .Include(p => p.Visits)
                              .ThenInclude(v => v.VisitProcedures)
                              .ThenInclude(m => m.Procedure)
                              .Include(p => p.Visits)
                              .ThenInclude(v => v.VisitMedications)
                              .ThenInclude(m => m.Medication)
                              .AsSplitQuery()
                              .AsNoTracking()
                              .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
      }

      #region Alternative impl with explicit loading

      // public override async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
      // {
      //    var patient = await base.GetByIdAsync(id, cancellationToken);
      //
      //    if (patient == null)
      //    {
      //       return null;
      //    }
      //
      //    await _context.Visits.Where(v => v.PatientId == id)
      //                  .LoadAsync(cancellationToken: cancellationToken);
      //
      //    if (patient is {VisitsIds: not null})
      //
      //    {
      //       await _context.Doctors.Where(d => patient.VisitsIds.Contains(d.Id))
      //                     .LoadAsync(cancellationToken: cancellationToken);
      //
      //       foreach (var visitId in patient.VisitsIds)
      //       {
      //          await _context.VisitProcedures.Include(vp => vp.Procedure).Where(vp => vp.VisitId == visitId)
      //                        .LoadAsync(cancellationToken: cancellationToken);
      //
      //          await _context.VisitMedications.Include(vm => vm.Medication).Where(vm => vm.VisitId == visitId)
      //                        .LoadAsync(cancellationToken: cancellationToken);
      //       }
      //    }
      //
      //    return patient;
      // }

      #endregion
   }
}