using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Data.Services;
using App.Common;
using App.Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    [Authorize(Policy = "AuthenticatedUser")]
    public class ProductController : Controller
    {
        #region Fields and Properties

        public ProductService ProductService { get; }

        #endregion

        #region CTOR

        public ProductController(ProductService productService)
        {
            this.ProductService = productService;
        }

        #endregion

        // GET: api/Product
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await ProductService.GetProductsAsync();

            if (result != null)
                return Ok(result);
            else
                return BadRequest(Message.INTERNAL_ERROR);
        }


        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest(Message.INVLID_DATA);

            var result = await ProductService.GetAsync(id);

            if (result != null)
                return Ok(result);
            else
                return BadRequest(Message.INTERNAL_ERROR);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await ProductService.Create(model);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.PRODUCT_CREATE_SUCCESS : Message.PRODUCT_CREATE_FAIL
            });
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await ProductService.Update(model);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.PRODUCT_UPDATE_SUCCESS : Message.PRODUCT_UPDATE_FAIL
            });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(Message.INVLID_DATA);

            bool succeded = await ProductService.Delete(id);

            return Ok(new
            {
                succeded,
                message = succeded ? Message.PRODUCT_DELETE_SUCCESS : Message.PRODUCT_DELETE_FAIL
            });
        }
    }
}