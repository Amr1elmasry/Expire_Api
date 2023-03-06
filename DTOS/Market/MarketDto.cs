using Expire_Api.DTOS.Category;
using Expire_Api.DTOS.Product;
using Expire_Api.DTOS.Seller;
using Expire_Api.Mapping;
using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Market
{
    public class MarketDto 
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public ICollection<ForeignProductDto> Products { get; set; }
        public ICollection<ForeignCategoryDto> Categories { get; set; }

        public SellerDto Seller { get; set; }
        public string SellerId { get; set; }
    }
}
