using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject {
    public class Staff {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Password {  get; set; }
        public string Role {  get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}