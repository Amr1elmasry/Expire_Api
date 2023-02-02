using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Market
{
    public class UpdateMarketDto
    {
        [Required]
        public string? SellerId { get; set; }
        [Required]
        public int MarketId { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }
    }
}
