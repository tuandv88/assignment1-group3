using System.ComponentModel.DataAnnotations;
using WebAPI.Validation;

namespace WebAPI.Dto {
    public class ProductDto {

        [Update(ErrorMessage = "ProductId is required for update")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
    }
}
