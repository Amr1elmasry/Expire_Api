using Expire_Api.DTOS.Category;
using Expire_Api.DTOS.Market;
using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Product
{
    public class ForeignProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string BarCode { get; set; }
        public double Price { get; set; }

        [Required]
        public CurrencyCode Currency { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime ExpireData { get; set; }
        public int DayesToReminderBeforExpire { get; set; }


        public int CategoryId { get; set; }
        public int MarketId { get; set; }
    }
}
