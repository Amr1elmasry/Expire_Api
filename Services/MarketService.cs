using Expire_Api.DTOS.Market;
using Expire_Api.Interface;
using Expire_Api.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Expire_Api.Services
{
    public class MarketService : BaseRepository<Market>, IMarketService
    {
        private readonly UserManager<Seller> _userManager;
        private readonly ISellerService _sellerService;
        public MarketService(ApplicationDbContext Context, UserManager<Seller> userManager, ISellerService sellerService) : base(Context)
        {
            _userManager = userManager;
            _sellerService = sellerService;
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

        public async Task<int> GetCountOfMarkets(string sellerId)
        {
            Expression<Func<Market, bool>> criteria = d => d.SellerId == sellerId;
            var markets = await CountWithCriteria(criteria);
            return markets;
        }

        public async Task<ReturnMarket> AddMarket(PostMarketDto marketDto)
        {
            var returnMarket = new ReturnMarket { Messege= string.Empty };
            var seller = await _sellerService.FindByIdWithData(marketDto.SellerId);
            if (seller == null) returnMarket.Messege = "Cant't Find Seller with this id";
            else
            {
                var marketsOfUser = seller.Markets;
                if (marketsOfUser is not null && marketsOfUser.Any(m => m.Name.Equals(marketDto.Name)))
                {
                    returnMarket.Messege = "There is another market with the same name!";
                }
                else
                {
                    var market = new Market
                    {
                        Name = marketDto.Name,
                        SellerId = marketDto.SellerId
                    };
                    var result = await Add(market);
                    CommitChanges();
                    returnMarket.Market = market;
                }
            }
            return returnMarket;
        }
        public async Task<ReturnMarket> MarketValidation(string SellerId , int MarketId , [Optional] string Name)
        {
            var returnValidation = new ReturnMarket
            {
                Messege = "Cant't Find Market with this id"
            };
            var seller = await _userManager.FindByIdAsync(SellerId);
            var market = await FindById(MarketId);
            if (seller == null) returnValidation.Messege = "Cant't Find Seller with this id";
            else if (market is not null)
            {
                returnValidation.Market = market;
                if (market.SellerId != SellerId) returnValidation.Messege = "This Market belongs to another seller!";
                else if (Name is not null && market.Name == Name) returnValidation.Messege = "No Changes are Found";
                else returnValidation.Messege = string.Empty;
            }
            return returnValidation;
        }
        public async Task<ReturnMarket> UpdateMarket(UpdateMarketDto marketDto)
        {
            var validate = await MarketValidation(marketDto.SellerId,marketDto.MarketId,marketDto.Name);
            if (validate.Messege == string.Empty && validate.Market is not null)
            {
                validate.Market.Name = marketDto.Name;
                await Update(validate.Market);
                CommitChanges();
            }
            return validate; 
        }
        public async Task<ReturnMarket> DeleteMarket(DeleteMarketDto marketDto)
        {
            var validate = await MarketValidation(marketDto.SellerId, marketDto.MarketId);

            if (validate.Messege == string.Empty && validate.Market is not null)
            {
                await Delete(validate.Market);
                CommitChanges();
            }
            return validate;
        }
    }
}
