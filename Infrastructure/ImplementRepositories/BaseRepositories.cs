using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplementRepositories
{
    public class BaseRepositories<TEntity> : IBaseRepositories<TEntity> where TEntity : class
    {
        protected IDbContext _IdbContext = null;
        protected DbSet<TEntity> _dbSet;
        protected DbContext _dbContext;

        protected DbSet<TEntity> DBSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }

                return _dbSet;
            }
        }
        public BaseRepositories(IDbContext dbContext)
        {
            _IdbContext = dbContext;
            _dbContext = (DbContext)dbContext;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IdbContext.CommitChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            var data = await DBSet.FindAsync(id);
            if (data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IdbContext.CommitChangesAsync();
                return entity;
            }
            return entity;
        }
    }
}
