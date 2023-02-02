using Expire_Api.DTOS.Market;
using Expire_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expire_Api.Interface
{
    public interface IMarketService : IBaseRepository<Market>
    {
        //Task<Market> GetById(int id);
        Task<Market> FindByIdWithData(int id);

        Task<IEnumerable<Market>> GetMarketsOfSeller(string sellerId);
        Task<IEnumerable<Market>> GetMarketsOfSellerWithData(string sellerId);
        Task<Market> AddMarket(PostMarketDto marketDto);
        Task<ReturnMarket> UpdateMarket(UpdateMarketDto marketDto);
        Task<ReturnMarket> DeleteMarket(DeleteMarketDto marketDto);
    }
}
