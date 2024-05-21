using BusinessObject;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;

namespace WebAPI.Service {
    public class OrderDetailService : IOrderDetailRepository {
        private readonly TDbContext _context;
        public OrderDetailService(TDbContext context) {
            _context = context;
        }
        public bool DeleteOrderDetailById(int id) {
            var od = _context.OrderDetails.SingleOrDefault(
                       od => od.ProductId == id);
            if (od != null) {
                _context.OrderDetails.Remove(od);
                _context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public List<OrderDetails> GetAll() {
            List<OrderDetails> orderDetails = _context.OrderDetails
                                                .Include(od => od.Order)
                                                .Include(od => od.Product)
                                                .ToList();
            return orderDetails;
        }

        public OrderDetails GetOrderDetailById(int id) {
            OrderDetails od = _context.OrderDetails
                .SingleOrDefault(x => x.OrderDetailId == id);
            return od;
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId) {
            var ods = _context.OrderDetails.Where(od => od.OrderId == orderId).ToList();
            return ods;
        }

        public bool SaveOrderDetails(OrderDetails od) {
            try {
                _context.OrderDetails.Add(od);
                _context.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public void UpdateOrderDetails(OrderDetails od) {
            _context.Entry<OrderDetails>(od).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
