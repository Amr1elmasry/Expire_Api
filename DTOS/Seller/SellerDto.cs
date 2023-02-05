using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Expire_Api.DTOS.Market;
using System.Text.Json.Serialization;

namespace Expire_Api.DTOS.Seller
{
    public class SellerDto
    {
        [Required]
        [MaxLength(120)]
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public byte[]? Image { get; set; }

        public Gender? Gender { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public ICollection<MarketDto> Markets { get; }
    }
}
