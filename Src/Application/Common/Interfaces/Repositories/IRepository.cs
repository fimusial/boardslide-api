using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoardSlide.API.Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task AddManyAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        void UpdateMany(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveMany(IEnumerable<TEntity> entities);
    }
}