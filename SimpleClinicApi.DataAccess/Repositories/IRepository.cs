using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public interface IRepository<TEntity> where TEntity : class
   {
      Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
      Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
      Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
      void Update(TEntity entity);
      void Remove(TEntity entity);
      Task SaveChangesAsync(CancellationToken cancellationToken = default);
   }
}