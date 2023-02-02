using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Seller
{
    public class SellerLogin
    {
        [Required, MaxLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(120)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
