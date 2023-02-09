using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Category
{
    public class CreateCategoryDto
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public int MarketId { get; set; }
        [Required]
        public string SellerId { get; set; }
    }
}
