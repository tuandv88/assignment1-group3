using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Service;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("/ass1/api/product/")]
    public class ProductController : ControllerBase {
        private readonly ProductService _productService;
        public ProductController(ProductService productService) {
            _productService = productService;
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts() {
            List<Product> products = _productService.GetProducts();
            List<ProductDto> result = products.Select(p => new ProductDto {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                CategoryName = p.Category.CategoryName,
            }).ToList();
            return result;
        }

        [HttpGet("detail/{id}")]
        public ActionResult<ProductDto> Details(int id) {
            Product p = _productService.GetProductById(id);
            if (p == null) {
                return NotFound();
            } else {
                return new ProductDto() {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    CategoryId = (int)p.CategoryId,
                    CategoryName = p.Category.CategoryName
                };
            }
        }

        [HttpPost("create")]
        [Authorize("Admin")]
        public ActionResult Create(ProductDto p) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                Product p1 = new Product() {
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    CategoryId = p.CategoryId
                };
                bool status = _productService.SaveProduct(p1);
                if (status) {
                    return Ok("Add Product Success");
                } else {
                    return BadRequest("Created faild");
                }
            }
        }
        [HttpPut("update")]
        [Authorize("Admin")]
        public ActionResult Update(ProductDto p) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                Product p1 = _productService.GetProductById(p.ProductId);
                if (p1 == null) {
                    return NotFound(); 
                }
                p1.ProductName = p.ProductName;
                p1.UnitPrice = p.UnitPrice;
                p1.CategoryId = p.CategoryId;

                _productService.UpdateProduct(p1);
                return Ok("Update success");
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize("Admin")]
        public ActionResult Delete(int id) {
            bool status = _productService.DeleteProductById(id);
            if (!status) {
                return NotFound();
            } else {
                return Ok("Delete success");
            }
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<ProductDto>> Search(string? searchString, decimal? startUnitPrice, decimal? endUnitPrice) {
            List<Product> products = _productService.GetProductByNameAndUnitPrice(searchString, startUnitPrice, endUnitPrice);
            List<ProductDto> result = products.Select(p => new ProductDto {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                CategoryName = p.Category.CategoryName,
            }).ToList();
            return result;
        }
    }
}
