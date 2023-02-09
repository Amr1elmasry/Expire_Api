using Expire_Api.DTOS.Seller;
using Expire_Api.Interface;
using Expire_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        


        [HttpGet("GetSellerById")]
        public async Task<IActionResult> GetSellerById(string sellerId)
        {
            var seller = await _sellerService.FindById(sellerId);
            if (seller is null) return BadRequest("No seller was found");
            return Ok(seller);
        }
        [HttpGet("GetSellerByIdWithData")]
        public async Task<IActionResult> GetSellerByIdWithData(string sellerId)
        {
            var seller = await _sellerService.FindById(sellerId);
            if (seller is null) return BadRequest("No seller was found");
            return Ok(seller);
        }
        
        [HttpGet("GetAllSellers")]
        public async Task<IActionResult> GetAllSellers()
        {
            var sellers = await _sellerService.GetAll();
            if (sellers is null) return BadRequest("No sellers were found");
            return Ok(sellers);
        }

        [HttpGet("GetAllSellersWithData")]
        public async Task<IActionResult> GetAllSellersWithData()
        {
            var sellers = await _sellerService.GetAllWithData();
            if (sellers is null) return BadRequest("No sellers were found");
            return Ok(sellers);
        }
    }
}
