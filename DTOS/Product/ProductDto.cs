using Expire_Api.DTOS.Category;
using Expire_Api.DTOS.Market;
using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Product
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string BarCode { get; set; }
        public double Price { get; set; }

        [Required]
        public CurrencyCode CurrencyCode { get; set; }
        public int Quantity { get; set; }

        public ForeignCategoryDto Category { get; set; }
        public int CategoryId { get; set; }

        public ForeignMarketDto Market { get; set; }
        public int MarketId { get; set; }
    }
}
