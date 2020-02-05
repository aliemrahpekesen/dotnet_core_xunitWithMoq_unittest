using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductMan.API.Domain;
using ProductMan.API.Domain.Model;
using ProductMan.API.Services;

namespace ProductMan.API.Controllers
{
    [Route("api/products/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IProductService _productService;

        public ProductController(ILogger logger, IProductService productService)
        {
            this._logger = logger;
            this._productService = productService;
        }

        // POST
        // api/products/
        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="request">PostProductRequest</param>
        /// <returns>A response with new product</returns>
        /// <response code="201">A response as creation of product</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostProductAsync([FromBody]PostProductRequest request)
        {
            ProductMapper mapper = new ProductMapper();
            this._logger?.LogDebug("'{0}' has been invoked", nameof(PostProductAsync));
            var isDuplicate = this._productService.DuplicateCheckByCode(request.Code);
            if (isDuplicate)
                ModelState.AddModelError("ProductCode", "Product already exists");
            if (!ModelState.IsValid)
                return BadRequest();
            var entity = mapper.ToEntity(request);
            var createdResource = await this._productService.Create(entity);
            return this.Ok(createdResource);
        }

        // GET
        // api/products/5
        /// <summary>
        /// Retrieves a product item by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>A response with product</returns>
        /// <response code="200">Returns the product</response>
        /// <response code="404">If product is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            this._logger?.LogDebug("'{0}' has been invoked", nameof(GetProductByIdAsync));

            var foundProduct = this._productService.RetrieveProductById(id).Result;

            if (foundProduct == null)
                return NotFound();

            return this.Ok(foundProduct);
        }

        // GET
        // api/products
        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <returns>A response with product list</returns>
        /// <response code="200">Returns the product list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductsAsync()
        {
            this._logger?.LogDebug("'{0}' has been invoked", nameof(GetProductsAsync));

            var productList = this._productService.RetrieveAllProducts();

            return this.Ok(productList);
        }

        // PUT
        // api/products/5

        /// <summary>
        /// Updates an existing product
        /// </summary>Product ID</param>
        /// <param name="request">PutProductRequest</param>
        /// <returns>A response as update product result</returns>
        /// <response code="200">If product was updated successfully</response>
        /// <response code="404">Id a product is not found by given id</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutProductAsync(int id, [FromBody]PutProductRequest request)
        {
            ProductMapper mapper = new ProductMapper();
            this._logger?.LogDebug("'{0}' has been invoked", nameof(PutProductAsync));
            var existingResource = this._productService.RetrieveProductById(id).Result;

            if (existingResource == null)
                return NotFound();

            var resourceToBeUpdated = mapper.ToEntity(id, request);

            var updatedResource = this._productService.Update(id, resourceToBeUpdated).Result;

            return this.Ok(updatedResource);
        }

        // DELETE
        // api/products/5
        /// <summary>Delete an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>A response as delete product result</returns>
        /// <response code="204">If product was deleted successfully</response>
        /// <response code="404">Id a product is not found by given id</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            this._logger?.LogDebug("'{0}' has been invoked", nameof(DeleteProductAsync));

            var existingResource = this._productService.RetrieveProductById(id).Result;

            if (existingResource == null)
                return NotFound();

            await this._productService.Delete(id);

            return this.NoContent();
        }
    }
}