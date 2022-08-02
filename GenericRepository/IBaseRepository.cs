using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IBaseRepository<T,Y> where T : class
    {
        Task<T> Create(T entity);
        Task<bool> Delete(T item);
        Task<bool> Delete(Y id);
        Task<T> FindById(Y id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        IQueryable<T> GetAll();
        Task<T> Update(T entity);
        IQueryable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties);
    }
}
