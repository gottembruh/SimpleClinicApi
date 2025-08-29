using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public class GenericRepository<TEntity, TContext>(TContext context) : IRepository<TEntity>
      where TEntity : class, new()
      where TContext : DbContext
   {
      protected readonly TContext _context = context;
      protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

      public async Task<IEnumerable<TEntity>> GetAllWithoutNavPropsAsync(CancellationToken cancellationToken = default)
      {
         return await _dbSet.ToListAsync(cancellationToken);
      }

      public async Task<TEntity?> GetByIdWithoutNavPropsAsync(Guid id, CancellationToken cancellationToken = default)
      {
         return await _dbSet.FindAsync([id], cancellationToken);
      }

      public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
      {
         await _dbSet.AddAsync(entity, cancellationToken);
      }

      public void Update(TEntity entity)
      {
         _dbSet.Update(entity);
      }

      public void Remove(TEntity entity)
      {
         _dbSet.Remove(entity);
      }

      public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
      {
         await _context.SaveChangesAsync(cancellationToken);
      }
   }
}