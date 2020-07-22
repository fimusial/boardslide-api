using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Application.Common.Interfaces.Repositories;

namespace BoardSlide.API.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext BaseContext;

        public Repository(DbContext context)
        {
            BaseContext = context;
        }

        public async Task<TEntity> GetAsync(int id) 
            => await BaseContext.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await BaseContext.Set<TEntity>().ToListAsync();
        
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await BaseContext.Set<TEntity>().Where(predicate).ToListAsync();
        
        public async Task<TEntity> AddAsync(TEntity entity)
            => (await BaseContext.Set<TEntity>().AddAsync(entity)).Entity;
        
        public async Task AddManyAsync(IEnumerable<TEntity> entities)
            => await BaseContext.Set<TEntity>().AddRangeAsync(entities);

        public TEntity Update(TEntity entity)
            => BaseContext.Set<TEntity>().Update(entity).Entity;

        public void UpdateMany(IEnumerable<TEntity> entities)
            => BaseContext.Set<TEntity>().UpdateRange(entities);
        
        public void Remove(TEntity entity)
            => BaseContext.Set<TEntity>().Remove(entity);
        
        public void RemoveMany(IEnumerable<TEntity> entities)
            => BaseContext.Set<TEntity>().RemoveRange(entities);
    }
}