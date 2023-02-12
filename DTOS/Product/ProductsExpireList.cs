using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Product
{
    public class ProductsExpireList
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string BarCode { get; set; }

        public DateTime ExpireData { get; set; }
        public int TimeToExpire { get; set; }

    }
}
