using Expire_Api.DTOS.Market;
using Expire_Api.Interface;
using Expire_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expire_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketService _marketService;

        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var markets = await _marketService.GetAll();
            if (markets== null) return NotFound("No Markets are found");
            return Ok(markets);
        }

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var markets = await _marketService.GetAllWithData();
            if (markets == null) return NotFound("No Markets are found");
            return Ok(markets);
        }

        [HttpGet("FindById")]
        public async Task<IActionResult> FindById(int marketId)
        {
            var markets = await _marketService.FindById(marketId);
            if (markets == null) return NotFound("No Markets are found");
            return Ok(markets);
        }

        [HttpGet("FindByIdWithData")]
        public async Task<IActionResult> FindByIdWithData(int marketId)
        {
            var markets = await _marketService.FindByIdWithData(marketId);
            if (markets == null) return NotFound("No Markets are found");
            return Ok(markets);
        }

        [HttpGet("GetMarketsOfSeller")]
        public async Task<IActionResult> GetMarketsOfSeller(string sellerId)
        {
            var markets = await _marketService.GetMarketsOfSeller(sellerId);
            if (markets == null) return NotFound("No Markets are found");
            return Ok(markets);
        }       

        [HttpGet("GetMarketsOfSellerWithData")]
        public async Task<IActionResult> GetMarketsOfSellerWithData(string sellerId)
        {
            var markets = await _marketService.GetMarketsOfSellerWithData(sellerId);
            if (markets == null) return NotFound("No Markets are found");
            return Ok(markets);
        }


        [HttpPost("AddMarket")]
        public async Task<IActionResult> AddMarket([FromBody] PostMarketDto marketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var market = await _marketService.AddMarket(marketDto);
            if (market.Id == 0) return BadRequest("Cant't Find Seller with this id");
            return Ok(market);
        }

        [HttpPut("UpdateMarket")]
        public async Task<IActionResult> UpdateMarket([FromBody] UpdateMarketDto marketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var market = await _marketService.UpdateMarket(marketDto);
            if (market.Messege != string.Empty) return BadRequest(market.Messege);
            return Ok(market.Market);
        }

        [HttpDelete("DeleteMarket")]
        public async Task<IActionResult> DeleteMarket([FromBody] DeleteMarketDto marketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var market = await _marketService.DeleteMarket(marketDto);
            if (market.Messege != string.Empty) return BadRequest(market.Messege);
            return Ok(market.Market);
        }
    }
}
