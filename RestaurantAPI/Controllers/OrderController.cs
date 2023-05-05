using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Infrastructure.Exceptions;
using RestaurantAPI.Services;
using RestaurantAPI.Services.Managers;
using RestaurantAPI.Services.UnitsOfWork;

namespace RestaurantAPI.Controllers
{
    [Route("Order")]
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
            try
            {
                var orderEntity = _orderService.GetOrder(id);
                return Ok(orderEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("", Name = "GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orderEntities = _orderService.GetAllOrders();
                return Ok(orderEntities);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ordered-by-address", Name = "GetAllOrdersOrderedByAddress")]
        [ProducesResponseType(typeof(PagedResult<OrderDTO>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IActionResult GetAllOrdersOrderedByAddress([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var orderEntities = _orderService.GetAllOrdersOrderedByAddress(pageNumber, pageSize);
                return Ok(orderEntities);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Details/{id}", Name = "GetOrderDetails")]
        public IActionResult GetOrderDetails(int id)
        {
            try
            {
                var orderEntity = _orderService.GetOrderDetails(id);
                return Ok(orderEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Add", Name = "AddOrder")]
        public IActionResult AddOrder([FromBody] OrderDTO order)
        {
            try
            {
                var orderEntity = _orderService.AddOrder(order);
                return Ok(orderEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var deleteOrder = _orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        [Route("{id}", Name = "UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] UpdateOrderDTO order)
        {
            try
            {
                var orderEntity = _orderService.UpdateOrder(order);
                return Ok(orderEntity);
            }
            catch (ResourceMissingException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
