using App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class DbRepository<T> : IDbRepository<T> where T : class
    {
        #region Fields and properties

        private readonly DbContext dbContext;
        private readonly DbSet<T> dbSet;

        #endregion

        #region CTOR

        public DbRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert item(s)
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Insert(params T[] entities)
        {
            try
            {
                for (int i = 0; i < entities.Length; i++)
                    dbSet.Add(entities[i]);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update item(s)
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(params T[] entities)
        {
            try
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    dbSet.Attach(entities[i]);
                    dbContext.Entry(entities[i]).State = EntityState.Modified;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete item(s) with matching key(s)
        /// </summary>
        /// <param name="keys">Primary key(s)</param>
        public virtual void Delete(params object[] keys)
        {
            try
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    T keyEntity = dbSet.Find(keys[i]);
                    if (keyEntity != null)
                        dbSet.Remove(keyEntity);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete item(s)
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Delete(params T[] entities)
        {
            try
            {
                for (int i = 0; i < entities.Length; i++)
                    dbSet.Remove(entities[i]);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete item(s) that matches condition
        /// </summary>
        /// <param name="filterExpression"></param>
        public virtual void Delete(Expression<Func<T, bool>> filterExpression)
        {
            try
            {
                IEnumerable<T> objects = dbSet.Where(filterExpression).AsEnumerable();
                foreach (T obj in objects)
                    dbSet.Remove(obj);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get item with matching key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(object key)
        {
            try
            {
                return await Task.Run(() => dbSet.Find(key));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get item that matches condition
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filterExpression)
        {
            try
            {
                return await dbSet.FirstOrDefaultAsync(filterExpression);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get items that matches conditions async
        /// </summary>
        /// <typeparam name="TSortKey"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            try
            {
                IQueryable<T> queryable = filterExpression != null ? dbSet.Where(filterExpression) : this.dbSet;

                return await queryable.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
