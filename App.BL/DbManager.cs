using App.DAL;
using App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BL
{
    class DbManager<T> : IDbManager<T> where T : class
    {
        #region Fields and properties

        private readonly DbContext dbContext;
        private readonly IDbRepository<T> dbRepository;

        #endregion

        #region CTOR

        public DbManager(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbRepository = new DbRepository<T>(this.dbContext);
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
                this.dbRepository.Insert(entities);
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
                this.dbRepository.Update(entities);
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
                this.dbRepository.Delete(keys);
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
                this.dbRepository.Delete(entities);
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
                this.dbRepository.Delete(filterExpression);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get item with matching key async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(object key)
        {
            try
            {
                return await this.dbRepository.GetAsync(key);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get item that matches condition async
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filterExpression)
        {
            try
            {
                return await this.dbRepository.GetAsync(filterExpression);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get items that matches condition async
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            try
            {
                return await this.dbRepository.GetManyAsync(filterExpression);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Save changes async
        /// </summary>
        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
