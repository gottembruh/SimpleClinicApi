using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories;

public class DoctorRepository(ClinicDbContext context)
    : GenericRepository<Doctor, ClinicDbContext>(context),
        IDoctorRepository
{
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        Set.AnyAsync(d => d.Id == id, cancellationToken);


    public async Task<Doctor?> GetByIdWholeAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) =>
        await Context
            .Doctors.Include(p => p.Visits)
            .ThenInclude(v => v.Patient)
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
