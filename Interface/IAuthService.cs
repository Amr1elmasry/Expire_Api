using Expire_Api.DTOS.Seller;
using Expire_Api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Expire_Api.Interface
{
    public interface IAuthService
    {
        Task<SellerReturnDto> SellerRegister(SellerRegister seller);
        Task<SellerReturnDto> SellerLogin(SellerLogin sellerLogin);
        Task<bool> AddRole(string roleName);
    }
}
