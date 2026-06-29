using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlowerShop.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return _set;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _set.ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(params object[] keyValues)
        {
            return _set.FindAsync(keyValues);
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _set.Where(predicate).ToListAsync();
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }
    }
}
