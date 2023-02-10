using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        public string BarCode { get; set; }
        public double Price { get; set; }

        [Required]
        public CurrencyCode CurrencyCode { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int MarketId { get; set; }
    }
}
