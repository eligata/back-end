using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BL
{
    public class UnitOfWork : IDisposable
    {
        #region Fields and properties

        private DbContext _dbContext;
        private HashSet<object> _managers;

        #endregion

        #region CTOR

        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
            _managers = new HashSet<object>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a specific manager instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDbManager<T> Manager<T>() where T : class
        {
            IDbManager<T> manager = _managers.FirstOrDefault(x => x.GetType().FullName == typeof(DbManager<T>).FullName) as IDbManager<T>;

            if (manager == null)
            {
                manager = new DbManager<T>(_dbContext);
                _managers.Add(manager);
            }

            return manager;
        }

        /// <summary>
        /// Saves changes
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves changes async
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        #endregion
    }
}
