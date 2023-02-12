using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Product
{
    public class DeleteProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? SellerId { get; set; }
    }
}
