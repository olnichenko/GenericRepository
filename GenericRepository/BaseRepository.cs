using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GenericRepository
{
    public abstract class BaseRepository<T, Y>: IBaseRepository<T, Y> where T : class
    {
        protected readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(DbContext appDbContext)
        {
            _dbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }
        public virtual async Task<T> Create(T entity)
        {
            _dbSet.Add(entity);
            await Save();
            return entity;
        }
        public virtual async Task<bool> Delete(T item)
        {
            _dbSet.Remove(item);
            await Save();
            return true;
        }
        public virtual async Task<bool> Delete(Y id)
        {
            var item = await FindById(id);
            await Delete(item);
            return true;
        }
        public virtual async Task<T> FindById(Y id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate);
        }
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        public virtual async Task<T> Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await Save();
            return entity;
        }
        public virtual IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }
        public virtual IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate);
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
