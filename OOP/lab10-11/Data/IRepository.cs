using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlowerShop.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(params object[] keyValues);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
