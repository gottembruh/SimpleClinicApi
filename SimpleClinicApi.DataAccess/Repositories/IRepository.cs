using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleClinicApi.DataAccess.Repositories
{
   public interface IRepository<TEntity> where TEntity : class
   {
      public Task<IEnumerable<TEntity>> GetAllWithoutNavPropsAsync(CancellationToken cancellationToken = default);
      public Task<TEntity?> GetByIdWithoutNavPropsAsync(Guid id, CancellationToken cancellationToken = default);
      public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
      public void Update(TEntity entity);
      public void Remove(TEntity entity);
      public Task SaveChangesAsync(CancellationToken cancellationToken = default);
   }
}