using App.API.Models;
using App.BL;
using App.Common.Entities;
using APP.API.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.API.Data.Services
{
    public class ProductService : Service<Product>
    {
        #region CTOR

        public ProductService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get list of products
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public async Task<List<ProductModel>> GetProductsAsync(Expression<Func<Product, bool>> filterExpression = null)
        {
            try
            {
                using (UnitOfWork uow = base.UnitOfWork as UnitOfWork)
                {
                    var result = await uow.Manager<Product>().GetManyAsync(filterExpression);
                    return result != null ? Mapper.Map<List<ProductModel>>(result) : new List<ProductModel>();
                }
            }
            catch (Exception e)
            {
                return LogException(e);
            }

        }

        #endregion
    }

}
