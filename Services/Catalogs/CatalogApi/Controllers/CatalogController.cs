using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Entities;
using CatalogApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("api/v1/catalogs")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(
            IProductRepository productRepository,
             ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            return (IEnumerable<Product>)Ok(products);
        }

        [HttpGet("{id:lenght(24)}", Name = nameof(GetProduct))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                _logger.LogError($"product with id : {id} is not found");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Product>> GetProducts(string category)
        {
            var products = await _productRepository.GetProductsByCategoryId(category);

            return (IEnumerable<Product>)Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
          return Ok(await _productRepository.Update(product));
        }

        [HttpDelete("{id;lenght(24)}",Name = nameof(Delete))]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] Product product)
        {
          return Ok(await _productRepository.Update(product));
        }
    }
}