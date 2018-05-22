using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Data.Services;
using App.API.Models;
using App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Basket")]
    [Authorize(Policy = "AuthenticatedUser")]
    public class BasketController : Controller
    {
        #region Fields and Properties

        public BasketService BasketService { get; }

        #endregion

        #region CTOR

        public BasketController(BasketService basketService)
        {
            BasketService = basketService;
        }

        #endregion

        // GET: api/Basket/5
        [HttpGet("{userId}", Name = "GetUsersBasket")]
        public async Task<IActionResult> Get(string userId)
        {
            var result = await BasketService.GetUsersBasket(userId);

            if (result != null)
                return Ok(result);
            else
                return BadRequest(Message.INTERNAL_ERROR);
        }

        // POST: api/Basket
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductToBasket([FromBody]ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await BasketService.AddProductToBasket(model);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.ADD_PRODUCT_TO_BASKET_SUCCESS : Message.ADD_PRODUCT_TO_BASKET_FAIL
            });
        }

        // POST: api/Basket
        [HttpPost("removeProduct")]
        public async Task<IActionResult> RemoveProductFromBasket([FromBody]ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await BasketService.RemoveProductFromBasket(model);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.REMOVE_PRODUCT_FROM_BASKET_SUCCESS : Message.REMOVE_PRODUCT_FROM_BASKET_FAIL
            });
        }

        // POST: api/Basket
        [HttpPost("updateProductQty")]
        public async Task<IActionResult> UpdateQtyOfProductInBasket([FromBody]ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await BasketService.UpdateQtyOfProductInBasket(model);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.UPDATE_PRODUCT_QTY_IN_BASKET_SUCCESS : Message.UPDATE_PRODUCT_QTY_IN_BASKET_FAIL
            });
        }

        // POST: api/Basket
        [HttpPost("purchaseBasket")]
        public async Task<IActionResult> PurchaseBasket([FromBody]int id)
        {
            if (id == 0)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await BasketService.PurchaseBasket(id);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.PURCHASE_BASKET_SUCCESS : Message.PURCHASE_BASKET_FAIL
            });
        }

        // DELETE: api/Basket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DiscardBasket(int id)
        {
            if (id == 0)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await BasketService.DiscardBasket(id);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.DISCARD_BASKET_SUCCESS : Message.DISCARD_BASKET_FAIL
            });
        }
    }
}