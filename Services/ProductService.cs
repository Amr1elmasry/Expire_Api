using Expire_Api.DTOS.Product;
using Expire_Api.Interface;
using Expire_Api.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Expire_Api.Services
{
    public class ProductService : BaseRepository<Product>, IProductService
    {
        private readonly IMarketService _marketService;
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext Context, IMarketService marketService, ApplicationDbContext context, ICategoryService categoryService) : base(Context)
        {
            _marketService = marketService;
            _context = context;
            _categoryService = categoryService;
        }

        public async Task<List<Product>> GetProductsOfMarket(int marketId)
        {
            Expression<Func<Product, bool>> expression = p=> p.MarketId == marketId;
            var products = await FindAll(expression);
            if (products is null || !products.Any()) 
                    return null;
            return products.ToList();
        }

        public async Task<List<Product>> GetProductsOfMarketWithData(int marketId)
        {
            Expression<Func<Product, bool>> expression = p => p.MarketId == marketId;
            var products = await FindAllWithData(expression);
            if (products is null || !products.Any())
                return null;
            return products.ToList();
        }

        private async Task<ReturnProduct> PostProductValidate (CreateProductDto productDto)
        {
            var returnProduct = new ReturnProduct { Messege = string.Empty };
            var market = await _marketService.FindByIdWithCustomData(productDto.MarketId, "Products");
            if (market == null) returnProduct.Messege = "No market found with this Id";
            else if (market.Products.Any(p => p.Name == productDto.Name))
                returnProduct.Messege = "There is another product with same name";
            else if (market.Products.Any(p=>p.BarCode == productDto.BarCode)) 
                returnProduct.Messege = "There is another product with same barcode";
            else
            {
                var category = await _categoryService.FindById(productDto.CategoryId);
                if (category is null) returnProduct.Messege = "No category found with this Id";
                else if (category.MarketId != productDto.MarketId)
                    returnProduct.Messege = "This market owned by another seller";
            }
            return returnProduct;
        }

        public async Task<ReturnProduct> AddProduct(CreateProductDto productDto)
        {
            var product = productDto.Adapt<Product>();
            var validate = await PostProductValidate(productDto);
            if (validate.Messege == string.Empty)
            {
                await Add(product);
                CommitChanges();
                validate.Product = product;
            }
            return validate;

        }

    }
}
