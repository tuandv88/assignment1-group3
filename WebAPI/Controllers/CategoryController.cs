using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Service;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("/ass1/api/category/")]
    public class CategoryController : ControllerBase {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories() {
            List<Category> categories = _categoryService.GetCategories();
            List<CategoryDto> rs = categories.Select(c => new CategoryDto {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).ToList();
            return rs;
        }

    }
}

