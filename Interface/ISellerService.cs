using Expire_Api.DTOS.Seller;
using Expire_Api.Models;

namespace Expire_Api.Interface
{
    public interface ISellerService : IBaseRepository<Seller>
    {
        Task<Seller> FindSellerById(string sellerId);
        Task<Seller> FindSellerByIdWithData(string sellerId);
        Task<SellerReturnDto> SellerRegister(SellerRegister seller);
        Task<SellerReturnDto> SellerLogin(SellerLogin sellerLogin);
        Task<bool> AddRole(string roleName);

    }
}
