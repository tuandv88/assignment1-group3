using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject {
    public class OrderDetails {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
