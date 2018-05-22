using App.BL;
using APP.API.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.API.Data.Services
{
    public class Service<T> where T : class
    {
        #region Fields and properties

        private readonly ApplicationDbContext _dbContext;
        private UnitOfWork _unitOfWork;

        protected UnitOfWork UnitOfWork { get { return _unitOfWork; } }
        public IMapper Mapper { get; }

        #endregion

        #region CTOR

        public Service(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._unitOfWork = new UnitOfWork(dbContext);
            this.Mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        ///  Get item of specific type with matching id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(object key)
        {
            try
            {
                return await UnitOfWork.Manager<T>().GetAsync(key);
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }

        /// <summary>
        /// Get item of specific type
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            try
            {
                return await UnitOfWork.Manager<T>().GetAsync(filterExpression);
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }

        /// <summary>
        /// Get all items of specific type
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            try
            {
                return await UnitOfWork.Manager<T>().GetManyAsync(filterExpression);
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }

        /// <summary>
        /// Create item(s) of specific type 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<bool> Create(params T[] entities)
        {
            try
            {
                using (UnitOfWork uow = UnitOfWork as UnitOfWork)
                {
                    uow.Manager<T>().Insert(entities);
                    await uow.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                LogException(e);
                return false;
            }
        }

        /// <summary>
        /// Update item(s) of specific type 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<bool> Update(params T[] entities)
        {
            try
            {
                using (UnitOfWork uow = UnitOfWork as UnitOfWork)
                {
                    uow.Manager<T>().Update(entities);
                    await uow.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                LogException(e);
                return false;
            }
        }

        /// <summary>
        /// Delete item of specific type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(object id)
        {
            try
            {
                using (UnitOfWork uow = UnitOfWork as UnitOfWork)
                {
                    uow.Manager<T>().Delete(id);
                    await uow.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception e)
            {
                LogException(e);
                return false;
            }
        }

        /// <summary>
        /// Delete item of specific type 
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(Expression<Func<T, bool>> filterExpression)
        {
            try
            {
                using (UnitOfWork uow = UnitOfWork as UnitOfWork)
                {
                    uow.Manager<T>().Delete(filterExpression);
                    await uow.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                LogException(e);
                return false;
            }
        }

        /// <summary>
        /// Log Exceptions
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual dynamic LogException(Exception e)
        {
            // TO DO - LOG
            return null;
        }

        #endregion
    }
}
