using BusinessObject;

namespace WebAPI.Repositories {
    public interface IOrderRepository {
        bool SaveOrder(Order o);
        Order GetOrderById(int id);
        bool DeleteOrderById(int id);
        void UpdateOrder(Order p);
        List<Order> GetOrders();
    }
}
