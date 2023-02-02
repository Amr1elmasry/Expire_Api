using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Market
{
    public class DeleteMarketDto
    {
        [Required]
        public string? SellerId { get; set; }
        [Required]
        public int MarketId { get; set; }
    }
}
