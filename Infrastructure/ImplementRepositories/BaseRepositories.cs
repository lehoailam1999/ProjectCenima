using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                DBSet.AddRangeAsync(entities);
                await _IdbContext.CommitChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var data = await DBSet.SingleOrDefaultAsync(predicate);
            return data; 
        }
        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public List<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return  _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
        public IQueryable<TEntity> WhereIQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var data =  DBSet.SingleOrDefault(predicate);
            return data;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DBSet.FindAsync(id);
        }
    }
}
