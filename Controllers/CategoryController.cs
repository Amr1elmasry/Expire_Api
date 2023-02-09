using Expire_Api.DTOS.Category;
using Expire_Api.DTOS.Market;
using Expire_Api.Interface;
using Expire_Api.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expire_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() 
        {
            var categories = await _categoryService.GetAll();
            if (categories == null || !categories.Any()) return NotFound("No categories were found");
            var result = categories.Adapt<IEnumerable<CategoryDto>>();
            return Ok(result);
        }

        [HttpGet("GetAllWithData")]
        public async Task<IActionResult> GetAllWithData()
        {
            var categories = await _categoryService.GetAllWithData();
            if (categories == null || !categories.Any()) return NotFound("No categories were found");
            var result = categories.Adapt<IEnumerable<CategoryDto>>();
            return Ok(result);
        }

        [HttpGet("FindById")]
        public async Task<IActionResult> FindById(int categoryId)
        {
            var category = await _categoryService.FindById(categoryId);
            if (category == null) return NotFound("No category was found");
            var result = category.Adapt<CategoryDto>();
            return Ok(result);
        }

        [HttpGet("FindByIdWithData")]
        public async Task<IActionResult> FindByIdWithData(int categoryId)
        {
            var category = await _categoryService.FindByIdWithData(categoryId);
            if (category == null) return NotFound("No category was found");
            var result = category.Adapt<CategoryDto>();
            return Ok(result);
        }

        [HttpGet("GetCategoriesOfMarket")]
        public async Task<IActionResult> GetCategoriesOfMarket(int marketId)
        {
            var categories = await _categoryService.GetCategoriesOfMarket(marketId);
            if (categories == null || !categories.Any() ) return NotFound("No category was found");
            var result = categories.Adapt<IEnumerable<CategoryDto>>();
            return Ok(result);
        }

        [HttpGet("GetCategoriesOfMarketWithData")]
        public async Task<IActionResult> GetCategoriesOfMarketWithData(int marketId)
        {
            var categories = await _categoryService.GetCategoriesOfMarketWithData(marketId);
            if (categories == null || !categories.Any()) return NotFound("No category was found");
            var result = categories.Adapt<IEnumerable<CategoryDto>>();
            return Ok(result);
        }

        [HttpPost("AddCategoryToMarket")]
        public async Task<IActionResult> AddCategoryToMarket(CreateCategoryDto categoryDto) 
        {
            var category = await _categoryService.AddCategory(categoryDto);
            if (category.Category is null || category.Messege != string.Empty) return BadRequest(category.Messege);
            var result = category.Category.Adapt<CategoryDto>();
            return Ok(result);
        }


        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var category = await _categoryService.UpdateCategory(categoryDto);
            if (category.Category is null || category.Messege != string.Empty) return BadRequest(category.Messege);
            var result = category.Category.Adapt<CategoryDto>();
            return Ok(result);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryDto categoryDto)
        {
            var category = await _categoryService.DeleteCategory(categoryDto);
            if (category.Category is null || category.Messege != string.Empty) return BadRequest(category.Messege);
            var result = category.Category.Adapt<CategoryDto>();
            return Ok(result);
        }


    }
}
