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
        Task<TEntity> UpdateAsync(int id, TEntity entity);
    }
}
