using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Seller
{
    public class SellerRegister
    {

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(120)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(120)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [MaxLength(120)]
        public string FullName { get; set; }

        
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNo { get; set; }

        public IFormFile? Image { get; set; }
        public Gender Gender { get; set; }
    }
}
