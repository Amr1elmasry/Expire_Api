using Expire_Api.Classes;
using Expire_Api.DTOS.Market;
using Expire_Api.Interface;
using Expire_Api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
            var result = markets.Adapt<IEnumerable<MarketDto>>();
            return Ok(result);
        }
        
        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var markets = await _marketService.GetAllWithData();
            if (markets == null) return NotFound("No Markets are found");
            var result = markets.Adapt<IEnumerable<MarketDto>>();
            return Ok(result);
        }

        [HttpGet("FindById")]
        public async Task<IActionResult> FindById(int marketId)
        {
            var market = await _marketService.FindById(marketId);
            if (market == null) return NotFound("No Markets are found");
            var result = market.Adapt<MarketDto>();
            return Ok(result);
        }

        [HttpGet("FindByIdWithData")]
        public async Task<IActionResult> FindByIdWithData(int marketId)
        {
            var market = await _marketService.FindByIdWithData(marketId);
            if (market == null) return NotFound("No Markets are found");
            var result = market.Adapt<MarketDto>();
            return Ok(result);
        }

        [HttpGet("GetMarketsOfSeller")]
        public async Task<IActionResult> GetMarketsOfSeller(string sellerId)
        {
            var markets = await _marketService.GetMarketsOfSeller(sellerId);
            if (markets == null) return NotFound("No Markets are found");
            var result = markets.Adapt<IEnumerable<MarketDto>>();
            return Ok(result);
        }       

        [HttpGet("GetMarketsOfSellerWithData")]
        public async Task<IActionResult> GetMarketsOfSellerWithData(string sellerId)
        {
            var markets = await _marketService.GetMarketsOfSellerWithData(sellerId);
            if (markets == null) return NotFound("No Markets are found");
            var result = markets.Adapt<IEnumerable<MarketDto>>();
            return Ok(result);
        }

        [HttpGet("GetCountOfMarkets")]
        public async Task<IActionResult> GetCountOfMarkets(string sellerId)
        {
            var count = await _marketService.GetCountOfMarkets(sellerId);
            return Ok(count);
        }


        [HttpPost("AddMarket")]
        public async Task<IActionResult> AddMarket([FromBody] PostMarketDto marketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var market = await _marketService.AddMarket(marketDto);
            if (market.Market is null || market.Messege != string.Empty) return BadRequest(market.Messege);
            var result = market.Market.Adapt<MarketDto>();
            return Ok(result);
        }

        [HttpPut("UpdateMarket")]
        public async Task<IActionResult> UpdateMarket([FromBody] UpdateMarketDto marketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var market = await _marketService.UpdateMarket(marketDto);
            if (market.Messege != string.Empty) return BadRequest(market.Messege);
            var result = market.Market.Adapt<MarketDto>();
            return Ok(result);
        }

        [HttpDelete("DeleteMarket")]
        public async Task<IActionResult> DeleteMarket([FromBody] DeleteMarketDto marketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var market = await _marketService.DeleteMarket(marketDto);
            if (market.Messege != string.Empty) return BadRequest(market.Messege);
            var result = market.Market.Adapt<MarketDto>();
            return Ok(result);
        }
    }
}
