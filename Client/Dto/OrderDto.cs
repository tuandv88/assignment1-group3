using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto {
    public class OrderDto {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int StaffId {  get; set; }
        public string StaffName { get; set; }
    }
}
