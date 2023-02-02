using Expire_Api.DTOS.Market;
using Expire_Api.Interface;
using Expire_Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Numerics;

namespace Expire_Api.Services
{
    public class MarketService : BaseRepository<Market>, IMarketService
    {
        private readonly UserManager<Seller> _userManager;
        public MarketService(ApplicationDbContext Context, UserManager<Seller> userManager) : base(Context)
        {
            _userManager = userManager;
        }

        //public async Task<Market> GetById(int id)
        //{
        //    var market = await FindById(id);
        //    if (market == null) return null;
        //    return market;
        //}

        public async Task<Market> FindByIdWithData(int id)
        {
            Expression<Func<Market, bool>> criteria = d => d.Id == id;
            var market = await FindWithData(criteria);
            if (market == null) return null;
            return market;
        }

        public async Task<IEnumerable<Market>> GetMarketsOfSeller(string sellerId)
        {
            Expression<Func<Market, bool>> criteria = d => d.SellerId == sellerId;
            var markets = await FindAll(criteria);
            if (markets == null) return null;
            return markets;
        }

        public async Task<IEnumerable<Market>> GetMarketsOfSellerWithData(string sellerId)
        {
            Expression<Func<Market, bool>> criteria = d => d.SellerId == sellerId;
            var markets = await FindAllWithData(criteria);
            if (markets == null) return null;
            return markets;
        }

        public async Task<Market> AddMarket(PostMarketDto marketDto)
        {
            var seller = await _userManager.FindByIdAsync(marketDto.SellerId);
            if (seller == null) return new Market { Id = 0 };
            var market = new Market
            {
                Name = marketDto.Name,
                SellerId = marketDto.SellerId
            };
            var result = await Add(market);
            CommitChanges();
            return result;
        }

        public async Task<ReturnMarket> UpdateMarket(UpdateMarketDto marketDto)
        {
            var returnMarket = new ReturnMarket
            {
                Market = new Market { },
                Messege = String.Empty
            };
            var seller = await _userManager.FindByIdAsync(marketDto.SellerId);
            if (seller == null)
            {
                returnMarket.Messege = "Cant't Find Seller with this id";
                return returnMarket;
            }
            var market = await FindById(marketDto.MarketId);
            if (market == null)
            {
                returnMarket.Messege = "Cant't Find Market with this id";
                return returnMarket;
            }
            if (market.SellerId != marketDto.SellerId)
            {
                returnMarket.Messege = "This Market belongs to another seller!";
                return returnMarket;
            }
            if (market.Name == marketDto.Name)
            {
                returnMarket.Messege = "No Changes are Found";
                return returnMarket;
            }
            market.Name = marketDto.Name;
            var result = await Update(market);
            CommitChanges();
            returnMarket.Market = market;
            return returnMarket; 
        }

        public async Task<ReturnMarket> DeleteMarket(DeleteMarketDto marketDto)
        {
            var returnMarket = new ReturnMarket
            {
                Market = new Market { },
                Messege = String.Empty
            };
            var seller = await _userManager.FindByIdAsync(marketDto.SellerId);
            if (seller == null)
            {
                returnMarket.Messege = "Cant't Find Seller with this id";
                return returnMarket;
            }
            var market = await FindById(marketDto.MarketId);
            if (market == null)
            {
                returnMarket.Messege = "Cant't Find Market with this id";
                return returnMarket;
            }
            if (market.SellerId != marketDto.SellerId)
            {
                returnMarket.Messege = "This Market belongs to another seller!";
                return returnMarket;
            }
            var result = await Delete(market);
            CommitChanges();
            returnMarket.Market = market;
            return returnMarket;
        }
    }
}
