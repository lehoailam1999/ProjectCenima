using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IBaseRepositories<TEntity> where TEntity : class
    {
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> FindAsync(params Object[] keyValues);
        Task<bool> DeleteAsync(int id);
        Task<List<TEntity>> GetAll();

    }
}
