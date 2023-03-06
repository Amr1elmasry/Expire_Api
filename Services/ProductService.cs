using Expire_Api.DTOS.Category;
using Expire_Api.DTOS.Product;
using Expire_Api.Interface;
using Expire_Api.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;



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
            Expression<Func<Product, bool>> expression = p => p.MarketId == marketId;
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

        public async Task<List<Product>> GetProductsOfMarketPagination(int marketId , int countInPage, int currentPage)
        {
            Expression<Func<Product, bool>> expression = p => p.MarketId == marketId;
            var products = await _context.Products.Where(expression)
                .Skip(countInPage * (currentPage-1))
                .Take(countInPage).ToListAsync();
            if (products is null || !products.Any())
                return null;
            return products.ToList();
        }


        public async Task<int> GetCountOfProducts(string sellerId)
        {
            Expression<Func<Product, bool>> criteria = d => d.SellerId == sellerId;
            var products = await CountWithCriteria(criteria);
            return products;
        }

        private async Task<ReturnProduct> CreateProductValidate(CreateProductDto productDto)
        {
            var returnProduct = new ReturnProduct { Messege = string.Empty };
            var market = await _marketService.FindByIdWithCustomData(productDto.MarketId, "Products");
            var product = productDto.Adapt<Product>();
            if (market == null || market.SellerId != productDto.SellerId) returnProduct.Messege = "No market found with this data";
            else if (market.Products.Any(p => p.Name == productDto.Name))
                returnProduct.Messege = "There is another product with same name";
            else if (market.Products.Any(p => p.BarCode == productDto.BarCode))
                returnProduct.Messege = "There is another product with same barcode";
            else
            {
                var category = await _categoryService.FindById(productDto.CategoryId);
                if (category is null) returnProduct.Messege = "No category found with this Id";
                else if (category.MarketId != productDto.MarketId)
                    returnProduct.Messege = "This category not exists";
                else
                {
                    if (product.DayesToReminderBeforExpire is 0)
                        product.DayesToReminderBeforExpire = category.DayesToReminderBeforExpire;
                    returnProduct.Product = product;

                }
            }

            return returnProduct;
        }

        public async Task<ReturnProduct> AddProduct(CreateProductDto productDto)
        {
            var validate = await CreateProductValidate(productDto);
            if (validate.Messege == string.Empty)
            {
                await Add(validate.Product);
                CommitChanges();
            }
            return validate;
        }

        private async Task<ReturnProduct> UpdateProductValidate(UpdateProductDto productDto)
        {
            var returnProduct = new ReturnProduct { Messege = string.Empty };
            var product = productDto.Adapt<Product>();
            var market = await _marketService.FindByIdWithCustomData(product.MarketId, "Categories");
            var check = await FindById(product.Id);
            if (check == null) returnProduct.Messege = "No product was found with this id";
            else if (JsonConvert.SerializeObject(product) == JsonConvert.SerializeObject(check)) returnProduct.Messege = "No Changes are found";
            else if (productDto.MarketId != check.MarketId || productDto.SellerId != check.SellerId)
                returnProduct.Messege = "You can't change sellerId or Market Id";
            else if (!market.Categories.Any(c => c.Id == productDto.CategoryId))
                returnProduct.Messege = "Your market doesn't contins this category";
            return returnProduct;

        }

        public async Task<ReturnProduct> UpdateProduct(UpdateProductDto productDto)
        {
            var product = productDto.Adapt<Product>();
            var validate = await UpdateProductValidate(productDto);
            if (validate.Messege == string.Empty)
            {
                await Update(product);
                CommitChanges();
                validate.Product = product;
            }
            return validate;
        }

        public async Task<ReturnProduct> DeleteProduct(DeleteProductDto productDto)
        {
            var returnProduct = new ReturnProduct { Messege = string.Empty };
            var product = await FindById(productDto.Id);
            if (product == null) returnProduct.Messege = "No product found with this Id";
            else if (product.SellerId != productDto.SellerId) returnProduct.Messege = "Error in sellerId";
            else
            {
                await Delete(product);
                CommitChanges();
                returnProduct.Product = product;
            }
            return returnProduct;
        }

        public async Task<IList<ProductsExpireList>> GetReminderExpiryProducts(string SellerId)
        {
            IList<ProductsExpireList> listOfProducts = new List<ProductsExpireList>();
            Expression<Func<Product, bool>> expression = p => p.SellerId == SellerId &&
                EF.Functions.DateDiffDay(DateTime.Now.Date , p.ExpireData) <= p.DayesToReminderBeforExpire+1 &&
                EF.Functions.DateDiffDay(DateTime.Now.Date, p.ExpireData) >= 0;
            var products = await FindAll(expression);
            if (products is not null)
            { 
                foreach (var product in products)
                {
                    if (product.ExpireData != DateTime.MinValue)
                    {
                        var time = (int)(product.ExpireData - DateTime.Now.Date).TotalDays;
                        if ((int)time == product.DayesToReminderBeforExpire ||
                                (!listOfProducts.Any(p => p.Id == product.Id)
                                && (time == product.DayesToReminderBeforExpire / 2 || time <= 3)))
                        {
                            listOfProducts.Add(new ProductsExpireList
                            {
                                Id = product.Id,
                                BarCode = product.BarCode,
                                Name = product.Name,
                                ExpireData = product.ExpireData,
                                TimeToExpire = time
                            });
                            product.DayesToReminderBeforExpire /= 2;
                        }
                    }
                }
            }
            return listOfProducts;
        }

        public async Task<IList<ProductsExpireList>> GetAllExpiryProducts(string SellerId)
        {
            IList<ProductsExpireList> listOfProducts = new List<ProductsExpireList>();
            Expression<Func<Product, bool>> expression = p => p.SellerId == SellerId &&
                EF.Functions.DateDiffDay(DateTime.Now.Date, p.ExpireData) <= p.DayesToReminderBeforExpire +1 &&
                EF.Functions.DateDiffDay(DateTime.Now.Date, p.ExpireData) >= 0;
            var products = await FindAll(expression);
            if (products != null)
            {
                foreach (var product in products)
                {
                    var time = (int)(product.ExpireData - DateTime.Now.Date).TotalDays;
                    listOfProducts.Add(new ProductsExpireList
                    {
                        Id = product.Id,
                        BarCode = product.BarCode,
                        Name = product.Name,
                        ExpireData = product.ExpireData,
                        TimeToExpire = time
                    });
                }
            }
            return listOfProducts;
        }
    }
}
