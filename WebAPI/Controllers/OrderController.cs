using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Service;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("/ass1/api/order/")]
    public class OrderController : ControllerBase {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService) {
            _orderService = orderService;
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<Order>> GetProducts() {
            return _orderService.GetOrders();
        }

        [HttpPost("create")]
        public ActionResult Create(OrderDto o) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                Order o1 = new Order() {
                    OrderDate = o.OrderDate,
                    StaffId = o.StaffId,
                };
                bool status = _orderService.SaveOrder(o1);
                if (status) {
                    return Ok("Add Order Success");
                } else {
                    return BadRequest("Add Order Fail");
                }
            }
        }
        [HttpPut("update")]
        public ActionResult Update(OrderDto o) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                Order o1 = _orderService.GetOrderById(o.OrderId);
                if (o1 == null) {
                    return NotFound();
                }
                o1.OrderDate = o.OrderDate;
                o1.StaffId = o.StaffId;

                _orderService.UpdateOrder(o1);
                return Ok("Update success");
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id) {
            bool status = _orderService.DeleteOrderById(id);
            if (!status) {
                return NotFound();
            } else {
                return Ok("Delete success");
            }
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<OrderDto>> Search(DateTime? orderDate) {
            List<Order> orders = _orderService.SearchOrderByOrderDate(orderDate);
            List<OrderDto> orderDtos = orders.Select(o => new OrderDto {
                OrderId = o.OrderID,
                OrderDate = o.OrderDate,
                StaffId = (int)o.StaffId,
                StaffName = o.Staff.Name
            }).ToList();
            return orderDtos;
        }

    }
}
