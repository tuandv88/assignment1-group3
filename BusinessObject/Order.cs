using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject {
    public class Order {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public int? StaffId {  get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        public ICollection<OrderDetails>? OrderDetails {get; set;}
        //Test - Hello
    }

}
