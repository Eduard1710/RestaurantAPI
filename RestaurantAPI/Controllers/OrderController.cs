using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Services;
using RestaurantAPI.Services.Managers;
using RestaurantAPI.Services.UnitsOfWork;

namespace RestaurantAPI.Controllers
{
    [Route("order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var orderEntity = _orderService.GetOrder(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            return Ok(orderEntity);
        }
        [HttpGet]
        [Route("", Name = "GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orderEntities = _orderService.GetAllOrders();
            if (orderEntities == null)
            {
                return NotFound();
            }
            return Ok(orderEntities);
        }
        [HttpGet]
        [Route("ordered-by-address", Name = "GetAllOrdersOrderedByAddress")]
        [ProducesResponseType(typeof(PagedResult<OrderDTO>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IActionResult GetAllOrdersOrderedByAddress([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var orderEntities = _orderService.GetAllOrdersOrderedByAddress(pageNumber, pageSize);
            if (orderEntities == null)
            {
                return NotFound();
            }
            return Ok(orderEntities);
        }

        [HttpGet]
        [Route("details/{id}", Name = "GetOrderDetails")]
        public IActionResult GetOrderDetails(int id)
        {
            var orderEntity = _orderService.GetOrderDetails(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            return Ok(orderEntity);
        }
        [HttpPost]
        [Route("add", Name = "AddOrder")]
        public IActionResult AddOrder([FromBody] OrderDTO order)
        {
            var orderEntity = _orderService.AddOrder(order);
            return Ok(orderEntity);
        }
        [HttpDelete]
        [Route("delete/{id}", Name = "DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            var deleteOrder = _orderService.DeleteOrder(id);
            if (deleteOrder == false)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut]
        [Route("{id}", Name = "UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] UpdateOrderDTO order)
        {
            var orderEntity = _orderService.UpdateOrder(order);
            return Ok(orderEntity);
        }
    }
}
