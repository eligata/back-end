using App.API.Models;
using App.BL;
using App.Common.Entities;
using APP.API.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Data.Services
{
    public class BasketService : Service<Basket>
    {
        #region CTOR

        public BasketService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns users basket
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<BasketModel> GetUsersBasket(string userId)
        {
            try
            {
                Basket basket = await GetAsync(x => x.UserId == userId && x.Purchased == false);
                return basket != null ? Mapper.Map<BasketModel>(basket) : new BasketModel();
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }

        /// <summary>
        /// Add product to basket
        /// If there is no basket, first create one and then add product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddProductToBasket(ProductModel model)
        {
            try
            {
                using (UnitOfWork uow = base.UnitOfWork as UnitOfWork)
                {
                    Basket basket = await uow.Manager<Basket>().GetAsync(x => x.UserId == model.UserId && x.Purchased == false);

                    ///
                    /// If user hasn't basket then create one
                    if (basket == null)
                    {
                        basket = new Basket
                        {
                            UserId = model.UserId
                        };

                        uow.Manager<Basket>().Insert(basket);
                        await uow.SaveChangesAsync();
                    }

                    ///
                    /// If product already exist in basket then updat equantity
                    var existingProduct = basket.ProductsInBasket != null ? basket.ProductsInBasket.FirstOrDefault(x => x.ProductId == model.ProductId) : null;

                    if (existingProduct != null)
                    {
                        existingProduct.Quantity += model.Quantity;
                    }
                    else
                    {
                        uow.Manager<ProductInBasket>().Insert(
                            new ProductInBasket
                            {
                                BasketId = basket.Id,
                                ProductId = model.ProductId,
                                Quantity = model.Quantity
                            });
                    }

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
        /// Remove product from basket
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> RemoveProductFromBasket(ProductModel model)
        {
            try
            {
                using (UnitOfWork uow = base.UnitOfWork as UnitOfWork)
                {
                    uow.Manager<ProductInBasket>().Delete(x => x.ProductId == model.ProductId && x.BasketId == model.BasketId);
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
        /// Update product quantity in basket
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateQtyOfProductInBasket(ProductModel model)
        {
            try
            {
                using (UnitOfWork uow = base.UnitOfWork as UnitOfWork)
                {
                    var productInBasket = await uow.Manager<ProductInBasket>().GetAsync(x => x.ProductId == model.ProductId && x.BasketId == model.BasketId);

                    if (productInBasket != null)
                        productInBasket.Quantity = model.Quantity;

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
        /// Purchase basket
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        public async Task<bool> PurchaseBasket(int basketId)
        {
            try
            {
                using (UnitOfWork uow = base.UnitOfWork as UnitOfWork)
                {
                    Basket basket = await uow.Manager<Basket>().GetAsync(basketId);
                    basket.Purchased = true;

                    uow.Manager<Basket>().Update(basket);
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
        /// Discard basket
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        public async Task<bool> DiscardBasket(int basketId)
        {
            try
            {
                using (UnitOfWork uow = base.UnitOfWork as UnitOfWork)
                {
                    uow.Manager<ProductInBasket>().Delete(x => x.BasketId == basketId);
                    uow.Manager<Basket>().Delete(x => x.Id == basketId);
                    await uow.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                LogException(e);
                return false;
            }
        }

        #endregion
    }
}
