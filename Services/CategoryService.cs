using Expire_Api.DTOS.Category;
using Expire_Api.Interface;
using Expire_Api.Models;
using Mapster;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Expire_Api.Services
{
    public class CategoryService : BaseRepository<Category>, ICategoryService
    {
        private readonly IMarketService _marketService;
        public CategoryService(ApplicationDbContext Context, IMarketService marketService) : base(Context)
        {
            _marketService = marketService;
        }
        public async Task<IEnumerable<Category>> GetCategoriesOfMarket(int Marketid)
        {
            Expression<Func<Category, bool>> criteria = c => c.MarketId == Marketid;
            var categories = await FindAll(criteria);
            if (categories is null || !categories.Any()) return null;
            return categories;
        }

        public async Task<IEnumerable<Category>> GetCategoriesOfMarketWithData(int Marketid)
        {
            Expression<Func<Category , bool>> criteria = c=> c.MarketId == Marketid;
            var categories = await FindAllWithData(criteria);
            if (categories is null || !categories.Any()) return null;
            return categories;
        }

        public async Task<bool> CategoryIsExists(string categoryName, int marketId)
        {
            Expression<Func<Category, bool>> criteria = d => d.MarketId == marketId && d.Name.Equals(categoryName);
            var category = await Find(criteria);
            if (category is null) return false;
            return true;
        }
        public async Task<ReturnCategory> AddCategory(CreateCategoryDto categoryDto)
        {
            var returnMarket = new ReturnCategory
            {
                Messege = string.Empty,
            };
            var categoryIsExist = await CategoryIsExists(categoryDto.Name, categoryDto.MarketId);
            var market = await _marketService.FindById(categoryDto.MarketId);
            if (market is null || market.SellerId != categoryDto.SellerId) returnMarket.Messege = "No market found with this data";
            else if(categoryIsExist) returnMarket.Messege = "There is another category with the same name";
            else
            {
                var category = categoryDto.Adapt<Category>();
                await Add(category);
                CommitChanges();
                returnMarket.Category = category;
            }
            return returnMarket;
        }
        private async Task<ReturnCategory> UpdateCategoryValidate(UpdateCategoryDto categoryDto)
        {
            var returnCategory = new ReturnCategory
            {
                Messege = string.Empty,
            };
            Expression<Func<Category, bool>> criteria = c => c.Id == categoryDto.Id && c.SellerId == categoryDto.SellerId;
            var category = await Find(criteria);
            if (category is null) returnCategory.Messege = "No category found with this details";
            else if (category.Name == categoryDto.Name) returnCategory.Messege = "No Changes are found";
            else returnCategory.Category = category;
            return returnCategory;
        }
        public async Task<ReturnCategory> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var valid = await UpdateCategoryValidate(categoryDto);
            if(valid.Messege == string.Empty && valid.Category is not null)
            {
                valid.Category.Name = categoryDto.Name;
                await Update(valid.Category);
                CommitChanges();
            }
            return valid;
        }

        public async Task<ReturnCategory> DeleteCategory(DeleteCategoryDto categoryDto)
        {
            var returnCategory = new ReturnCategory
            {
                Messege = string.Empty,
            };
            var category = await FindByIdWithData(categoryDto.Id);
            if (category == null) returnCategory.Messege = "No category found with this Id";
            else if (category.SellerId != categoryDto.SellerId) returnCategory.Messege = "Error in sellerId";
            else
            {
                await Delete(category);
                CommitChanges();
                returnCategory.Category = category;
            }
            return returnCategory;
        }
    }
}
