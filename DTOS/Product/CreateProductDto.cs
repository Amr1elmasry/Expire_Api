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
        public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.EGP;
        public int Quantity { get; set; }
        public DateTime ExpireData { get; set; }
        public int DayesToReminderBeforExpire { get; set; }


        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int MarketId { get; set; }

        [Required]
        public string SellerId { get; set; }

    }
}
