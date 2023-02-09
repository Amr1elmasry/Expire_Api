using Expire_Api.DTOS.Category;
using Expire_Api.Models;

namespace Expire_Api.Interface
{
    public interface ICategoryService : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesOfMarket(int Marketid);
        Task<IEnumerable<Category>> GetCategoriesOfMarketWithData(int Marketid);
        Task<ReturnCategory> AddCategory(CreateCategoryDto categoryDto);
        Task<ReturnCategory> UpdateCategory(UpdateCategoryDto categoryDto);
        Task<ReturnCategory> DeleteCategory(DeleteCategoryDto categoryDto);
        Task<bool> CategoryIsExists(string categoryName, int marketId);
    }
}
