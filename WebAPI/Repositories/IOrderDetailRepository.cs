using BusinessObject;

namespace WebAPI.Repositories {
    public interface IOrderDetailRepository {
        bool SaveOrderDetails(OrderDetails od);
        OrderDetails GetOrderDetailById(int id);
        bool DeleteOrderDetailById(int id);
        void UpdateOrderDetails(OrderDetails od);
        List<OrderDetails> GetOrderDetailsByOrderId(int orderId);
        List<OrderDetails> GetAll();
    }       
}
