using Expire_Api.DTOS.Product;
using Expire_Api.Models;

namespace Expire_Api.Interface
{
    public interface IProductService : IBaseRepository<Product>
    {
        Task<List<Product>> GetProductsOfMarket(int marketId);
        Task<List<Product>> GetProductsOfMarketWithData(int marketId);
        Task<ReturnProduct> AddProduct(CreateProductDto productDto);
        Task<ReturnProduct> UpdateProduct(UpdateProductDto productDto);
        Task<ReturnProduct> DeleteProduct(DeleteProductDto productDto);
        Task<IList<ProductsExpireList>> GetExpiryProducts(string SellerId);
    }
}
