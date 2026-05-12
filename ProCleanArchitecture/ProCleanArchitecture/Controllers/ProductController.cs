using Microsoft.AspNetCore.Mvc;
using ProCleanArchitecture.Application.Interfaces;
using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _service.GetProductsAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _service.AddProductAsync(product);

            return Ok();
        }
    }
}