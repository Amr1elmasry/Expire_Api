using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Category
{
    public class DeleteCategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? SellerId { get; set; }
    }
}
