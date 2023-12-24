using Microsoft.AspNetCore.Mvc;
using OnlineShop.Base.Entities;
using OnlineShop.DAL.Domain;
using OnlineShop.DAL.Interfaces;

namespace OnlineShop.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IDataRepository _dataRepository;
        private ILogger _log;

        public ProductController(IDataRepository dataRepository, ILogger<ProductController> log)
        {
            _dataRepository = dataRepository;
            _log = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _dataRepository.GetProducts();

            if (products.Count() == 0)
                return NoContent();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(long id, [FromServices] ILogger<ProductController> logger)
        {
            logger.LogDebug("GetProduct Action Invoked");
            var product = await _dataRepository.GetProduct(id);

            if (product == null)
                return NoContent();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO product)
        {
            var addedProduct = await _dataRepository.AddProduct(product);
            if (addedProduct == null)
                return NoContent();

            return CreatedAtAction(nameof(AddProduct), new(), addedProduct);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO product)
        {
            var addedProduct = await _dataRepository.AddProduct(product);
            if (addedProduct == null)
                return NoContent();

            return CreatedAtAction(nameof(UpdateProduct), new(), addedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            bool result = await _dataRepository.DeleteProduct(id);
            if (result)
                return Ok();
            else
                return NotFound();

        }
    }
}
