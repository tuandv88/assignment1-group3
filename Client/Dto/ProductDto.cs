using System.ComponentModel.DataAnnotations;
namespace WebAPI.Dto {
    public class ProductDto {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
    }
}
