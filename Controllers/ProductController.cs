using Expire_Api.DTOS.Market;
using Expire_Api.DTOS.Product;
using Expire_Api.Interface;
using Expire_Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expire_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;
        public ProductController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet("GetProductsInMarket")]
        public async Task<IActionResult> GetProductsInMarket(int marketId)
        {
            var products = await _productService.GetProductsOfMarket(marketId);
            if (products == null)  return NotFound();
            var result = products.Adapt<IEnumerable<ProductDto>>();
            return Ok(result);
        }


        [HttpGet("GetProductsInMarketWithData")]
        public async Task<IActionResult> GetProductsInMarketWithData(int marketId)
        {
            var products = await _productService.GetProductsOfMarketWithData(marketId);
            if (products == null)  return NotFound();
            var result = products.Adapt<IEnumerable<ProductDto>>();
            return Ok(result);
        }

        [HttpPost("AddProductToMarket")]
        public async Task<IActionResult> AddProductToMarket([FromBody] CreateProductDto productDto)
        {
            var product = await _productService.AddProduct(productDto);
            if (product.Product == null || product.Messege != string.Empty)
                return BadRequest(product.Messege);
            var result = product.Product.Adapt<ProductDto>();
            return Ok(result);
        }
    }
}
