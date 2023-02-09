using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public string SellerId { get; set; }

    }
}
