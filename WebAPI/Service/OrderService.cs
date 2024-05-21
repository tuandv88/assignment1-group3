using BusinessObject;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;

namespace WebAPI.Service {
    public class OrderService : IOrderRepository {
        private readonly TDbContext _context;
        public OrderService(TDbContext context) {
            _context = context;
        }
        public bool DeleteOrderById(int id) {
            var o = _context.Orders.SingleOrDefault(
                        o => o.OrderID == id);
            if (o != null) {
                _context.Orders.Remove(o);
                _context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public Order GetOrderById(int id) {
            Order p = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .SingleOrDefault(x => x.OrderID == id);
            return p;
        }

        public List<Order> GetOrders() {
            return _context.Orders.ToList();
        }

        public bool SaveOrder(Order o) {
            try {
                _context.Orders.Add(o);
                _context.SaveChanges();
                return true;
            }catch (Exception ex) {
                return false;
            }
        }

        public void UpdateOrder(Order o) {
            _context.Entry<Order>(o).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Order> SearchOrderByOrderDate(DateTime? orderDate) {
            List<Order> orders = new List<Order>();
            if (orderDate != null) {
                orders = _context.Orders.Include(o => o.Staff).Where(o => o.OrderDate == orderDate).ToList();

            } else {
                _context.Orders.Include(o => o.Staff).ToList();
            }
            return orders;
        }
    }
}
