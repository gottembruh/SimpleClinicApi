using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories;

public class PatientRepository(ClinicDbContext context)
    : GenericRepository<Patient, ClinicDbContext>(context),
        IPatientRepository
{
    public override async Task AddAsync(
        Patient entity,
        CancellationToken cancellationToken = default
    )
    {
        var isDuplicated = await Set.AnyAsync(
            p => p.Equals(entity),
            cancellationToken: cancellationToken
        );

        if (isDuplicated)
        {
            throw new InvalidOperationException($"Patient with id {entity.Id} already exists.");
        }

        await base.AddAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetAllWithVisitsAsync(
        CancellationToken cancellationToken = default
    ) =>
        await Set.Include(x => x.Visits)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

    public async Task<Patient?> GetByIdWholeAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        await Context
            .Patients.Include(p => p.Visits)
            .ThenInclude(v => v.Doctor)
            .Include(p => p.Visits)
            .ThenInclude(v => v.VisitProcedures)
            .ThenInclude(m => m.Procedure)
            .Include(p => p.Visits)
            .ThenInclude(v => v.VisitMedications)
            .ThenInclude(m => m.Medication)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

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
