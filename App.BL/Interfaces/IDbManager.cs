using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BL
{
    public interface IDbManager<T> where T : class
    {
        /// <summary>
        /// Insert item(s)
        /// </summary>
        /// <param name="entities"></param>
        void Insert(params T[] entities);

        /// <summary>
        /// Update item(s)
        /// </summary>
        /// <param name="entities"></param>
        void Update(params T[] entities);

        /// <summary>
        /// Delete item(s) with matching key(s)
        /// </summary>
        /// <param name="keys">Primary key</param>
        void Delete(params object[] keys);

        /// <summary>
        /// Delete item(s)
        /// </summary>
        /// <param name="entities"></param>
        void Delete(params T[] entities);

        /// <summary>
        /// Delete item(s) that matches condition
        /// </summary>
        /// <param name="filterExpression"></param>
        void Delete(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Get item with matching key async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync(object key);

        /// <summary>
        /// Get item that matches condition async
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Get items that matches condition async
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> filterExpression = null);

        /// <summary>
        /// Save changes Async
        /// </summary>
        Task SaveChangesAsync();
    }
}
