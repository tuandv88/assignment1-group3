using System.ComponentModel.DataAnnotations;
using WebAPI.Validation;

namespace WebAPI.Dto {
    public class OrderDto {
        [Update(ErrorMessage = "OrderId is required for update")]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "StaffId is required")]
        public int StaffId {  get; set; }
        public string StaffName { get; set; }
    }
}
