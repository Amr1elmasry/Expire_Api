using Expire_Api.DTOS.Seller;
using Expire_Api.Models;

namespace Expire_Api.Interface
{
    public interface ISellerService 
    {
        Task<SellerReturnDto> SellerRegister(SellerRegister seller);
        Task<SellerReturnDto> SellerLogin(SellerLogin sellerLogin);
        Task<bool> AddRole(string roleName);

    }
}
