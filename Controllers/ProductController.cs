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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            if (products == null) return NotFound();
            var result = products.Adapt<IEnumerable<ProductDto>>();
            return Ok(result);
        }

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var products = await _productService.GetAllWithData();
            if (products == null) return NotFound();
            var result = products.Adapt<IEnumerable<ProductDto>>();
            return Ok(result);
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

        [HttpGet("FindProductById")]
        public async Task<IActionResult> FindProductById(int id)
        {
            var product = await _productService.FindById(id);
            if (product == null) return NotFound();
            var result = product.Adapt<ProductDto>();
            return Ok(result);
        }

        [HttpGet("FindProductByIdWithData")]
        public async Task<IActionResult> FindProductByIdWithData(int id)
        {
            var product = await _productService.FindByIdWithData(id);
            if (product == null) return NotFound();
            var result = product.Adapt<ProductDto>();
            return Ok(result);
        }

        [HttpGet("GetExpiryProducts")]
        public async Task<IActionResult> GetExpiryProducts(string sellerId)
        {
            var products = await _productService.GetExpiryProducts(sellerId);
            if (products == null) return NotFound();
            return Ok(products);
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

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto productDto)
        {
            var product = await _productService.UpdateProduct(productDto);
            if (product.Product == null || product.Messege != string.Empty )
                return BadRequest(product.Messege);
            var result = product.Product.Adapt<ProductDto>();
            return Ok(result);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductDto productDto)
        {
            var product = await _productService.DeleteProduct(productDto);
            if (product.Product == null || product.Messege != string.Empty)
                return BadRequest(product.Messege);
            var result = product.Product.Adapt<ProductDto>();
            return Ok(result);
        }
    }
}
