using Expire_Api.DTOS.Market;
using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Expire_Api.DTOS.Seller
{
    public class ForeignSellerDto
    {
        [Required]
        [MaxLength(120)]
        public string? FullName { get; set; } 

        public string? Address { get; set; }

        public byte[]? Image { get; set; }

    }
}
