using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Service;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("/ass1/api/orderdetail/")]
    public class OrderDetailsController : ControllerBase {
        private readonly OrderDetailService _orderDetailService;
        public OrderDetailsController(OrderDetailService orderDetailService) {
            _orderDetailService = orderDetailService;
        }
        [HttpGet("all/{orderId}")]
        public ActionResult<IEnumerable<OrderDetails>> GetOrderDetails(int orderId) {
            return _orderDetailService.GetOrderDetailsByOrderId(orderId);
        }

        [HttpGet("detail/{id}")]
        public ActionResult<OrderDetailDto> Details(int id) {
            OrderDetails od = _orderDetailService.GetOrderDetailById(id);
            if (od == null) {
                return NotFound();
            } else {
                return new OrderDetailDto() {
                    //TODO
                };
            }
        }

        [HttpPost("create")]
        public ActionResult Create(OrderDetailDto od) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                OrderDetails od1 = new OrderDetails() {
                    //TODO
                };
                bool status = _orderDetailService.SaveOrderDetails(od1);
                if (status) {
                    return Ok("Add OrderDetail Success");
                } else {
                    return BadRequest("Create faild");
                }
            }
        }
        [HttpPut("update")]
        public ActionResult Update(OrderDetailDto od) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else {
                OrderDetails od1 = _orderDetailService.GetOrderDetailById(od.OrderDetaildId);
                if (od1 == null) {
                    return NotFound();
                }
                //TODO

                _orderDetailService.UpdateOrderDetails(od1);
                return Ok("Update success");
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id) {
            bool status = _orderDetailService.DeleteOrderDetailById(id);
            if (!status) {
                return NotFound();
            } else {
                return Ok("Delete success");
            }
        }
        [HttpGet("list_all_by_date")]
        public ActionResult<IEnumerable<OrderDetailDto>> Reports(DateTime? startDate, DateTime? endDate) {
            DateTime a = DateTime.Now;
            List<OrderDetails> orderDetails = _orderDetailService.GetAll();
            if (startDate == null || endDate == null) {
                endDate = DateTime.Now;
                orderDetails = orderDetails.Where(o => o.Order.OrderDate >= endDate?.AddDays(-30) && o.Order.OrderDate <= endDate).ToList();
            } else {
                orderDetails = orderDetails.Where(o => o.Order.OrderDate >= startDate && o.Order.OrderDate <= endDate).ToList();
            }
            List<OrderDetailDto> rs = orderDetails.Select(od => new OrderDetailDto {
                OrderId = (int)od.OrderId,
                OrderDate = od.Order.OrderDate,
                StaffId = (int)od.Order.StaffId,
                StaffName = od.Order.Staff.Name,

                OrderDetaildId = od.OrderDetailId,
                ProductId = (int)od.ProductId,
                ProductName = od.Product.ProductName,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
            }).ToList();
            return rs;
        
        }
    }
}
