namespace Expire_Api.Models
{
    public class Seller : ApplicationUser
    {
        public ICollection<Market> Markets { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
