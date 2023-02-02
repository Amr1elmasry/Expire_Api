using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Market
{
    public class PostMarketDto
    {
        [Required]
        public string? SellerId { get; set; }

        [Required , MaxLength(100)]
        public string? Name { get; set; }

    }
}
