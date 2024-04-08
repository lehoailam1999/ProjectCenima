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


        //CRUD

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IdbContext.CommitChangesAsync();
            return entity;
        }       
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            DBSet.Add(entity);
            await _IdbContext.CommitChangesAsync();
            return entity;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var data = await DBSet.FindAsync(keyValues);
            return data;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dataEntity = await DBSet.FindAsync(id);
                if (dataEntity != null)
                {
                    DBSet.Remove(dataEntity);
                    await _IdbContext.CommitChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {

                return false; ;
            }
           

            
        }
        public async Task<List<TEntity>> GetAll()
        {
            var listData = await DBSet.ToListAsync();
            return listData;
        }

       
    }
}
