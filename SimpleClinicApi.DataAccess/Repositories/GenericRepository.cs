using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleClinicApi.DataAccess.Repositories;

public class GenericRepository<TEntity, TContext>(TContext context) : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    protected TContext Context { get; } = context;

    protected DbSet<TEntity> Set { get; } = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllWithoutNavPropsAsync(
        CancellationToken cancellationToken = default
    ) => await Set.ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIdWithoutNavPropsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => await Set.FindAsync([id], cancellationToken);

    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    ) => await Set.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity) => Set.Update(entity);

    public void Remove(TEntity entity) => Set.Remove(entity);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await Context.SaveChangesAsync(cancellationToken);
}
