using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IBaseRepositories<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> FindAsync(params Object[] keyValues);
        Task<bool> DeleteAsync(int id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity,bool>> predicate);
       TEntity SingleOrDefault(Expression<Func<TEntity,bool>> predicate);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> WhereIQueryable(Expression<Func<TEntity, bool>> predicate);

    }
}
