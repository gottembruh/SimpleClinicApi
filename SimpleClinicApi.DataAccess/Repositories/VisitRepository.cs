using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories;

public class VisitRepository(ClinicDbContext context)
    : GenericRepository<Visit, ClinicDbContext>(context),
        IVisitRepository
{
    public async Task<ILookup<Patient, Visit>> GetPatientsToVisitsLookupAsync(
        CancellationToken cancellationToken = default
    )
    {
        var visitsWithPatients = await Context
            .Visits.Include(v => v.Patient)
            .ToListAsync(cancellationToken);

        var lookup = visitsWithPatients.ToLookup(v => v.Patient, v => v);

        return lookup;
    }

    public async Task<Visit?> GetVisitWithDetailsAsync(
        Guid visitId,
        CancellationToken cancellationToken
    )
    {
        return await Context
            .Visits.Include(v => v.Doctor)
            .Include(v => v.Patient)
            .Include(v => v.VisitProcedures)
            .Include(v => v.VisitMedications)
            .FirstOrDefaultAsync(v => v.Id == visitId, cancellationToken);
    }

    public Task AddVisitProceduresRangeAsync(
        IEnumerable<VisitProcedure> items,
        CancellationToken ct
    )
    {
        return Context.VisitProcedures.AddRangeAsync(items, ct);
    }

    public Task AddVisitMedicationsRangeAsync(
        IEnumerable<VisitMedication> items,
        CancellationToken ct
    )
    {
        return Context.VisitMedications.AddRangeAsync(items, ct);
    }

    public void RemoveVisitProcedures(Visit visit, IEnumerable<VisitProcedure> proceduresToKeep)
    {
        var existing = visit.VisitProcedures.ToList();
        var toRemove = existing.Where(ep => proceduresToKeep.All(p => p.Id != ep.Id)).ToList();

        if (toRemove.Count != 0)
        {
            Context.VisitProcedures.RemoveRange(toRemove);
        }
    }

    public async Task AddOrUpdateVisitProceduresAsync(
        Visit visit,
        IEnumerable<VisitProcedure> procedures,
        CancellationToken cancellationToken
    )
    {
        var existing = visit.VisitProcedures;

        var toUpdate = existing.Where(ep => procedures.Any(p => p.Id == ep.Id)).ToList();
        IEnumerable<VisitProcedure> visitProcedures =
            procedures as VisitProcedure[] ?? procedures.ToArray();
        var toAdd = visitProcedures.Where(p => existing.All(ep => ep.Id != p.Id)).ToList();

        foreach (var proc in toUpdate)
        {
            var updatedProc = visitProcedures.First(p => p.Id == proc.Id);
            proc.ProcedureId = updatedProc.ProcedureId;
            proc.Notes = updatedProc.Notes;
        }

        if (toAdd.Count != 0)
        {
            await Context.VisitProcedures.AddRangeAsync(toAdd, cancellationToken);
        }
    }

    public void RemoveVisitMedications(Visit visit, IEnumerable<VisitMedication> medicationsToKeep)
    {
        var existing = visit.VisitMedications;
        var toRemove = existing.Where(em => medicationsToKeep.All(m => m.Id != em.Id)).ToList();

        if (toRemove.Count != 0)
        {
            Context.VisitMedications.RemoveRange(toRemove);
        }
    }

    public async Task AddOrUpdateVisitMedicationsAsync(
        Visit visit,
        IEnumerable<VisitMedication> medications,
        CancellationToken cancellationToken
    )
    {
        var existing = visit.VisitMedications.ToList();

        var toUpdate = existing.Where(em => medications.Any(m => m.Id == em.Id)).ToList();
        IEnumerable<VisitMedication> visitMedications =
            medications as VisitMedication[] ?? medications.ToArray();
        var toAdd = visitMedications.Where(m => existing.All(em => em.Id != m.Id)).ToList();

        foreach (var med in toUpdate)
        {
            var updatedMed = visitMedications.First(m => m.Id == med.Id);
            med.MedicationId = updatedMed.MedicationId;
            med.Dosage = updatedMed.Dosage;
            med.Notes = updatedMed.Notes;
        }

        if (toAdd.Count != 0)
        {
            await Context.VisitMedications.AddRangeAsync(toAdd, cancellationToken);
        }
    }
}
