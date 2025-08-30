using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimpleClinicApi.Domain.Models;

namespace SimpleClinicApi.DataAccess.Repositories;

public interface IVisitRepository : IRepository<Visit>
{
    public Task<ILookup<Patient, Visit>> GetPatientsToVisitsLookupAsync(
        CancellationToken cancellationToken = default
    );

    public Task<Visit?> GetVisitWithDetailsAsync(Guid visitId, CancellationToken cancellationToken);

    public Task AddVisitProceduresRangeAsync(
        IEnumerable<VisitProcedure> procedures,
        CancellationToken ct
    );

    public Task AddVisitMedicationsRangeAsync(
        IEnumerable<VisitMedication> medications,
        CancellationToken ct
    );

    public void RemoveVisitProcedures(Visit visit, IEnumerable<VisitProcedure> proceduresToKeep);

    public Task AddOrUpdateVisitProceduresAsync(
        Visit visit,
        IEnumerable<VisitProcedure> procedures,
        CancellationToken cancellationToken
    );

    public void RemoveVisitMedications(Visit visit, IEnumerable<VisitMedication> medicationsToKeep);

    public Task AddOrUpdateVisitMedicationsAsync(
        Visit visit,
        IEnumerable<VisitMedication> medications,
        CancellationToken cancellationToken
    );
}
