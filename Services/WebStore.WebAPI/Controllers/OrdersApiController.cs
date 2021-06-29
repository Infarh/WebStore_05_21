using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    /// <summary>Управление заказами</summary>
    [Route(WebAPIAddress.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        /// <summary>Получение списка заказов указанного по имени пользователя</summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Список заказов</returns>
        [HttpGet("user/{UserName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDTO>))]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await _OrderService.GetUserOrders(UserName);
            return Ok(orders.ToDTO());
        }

        /// <summary>Получение заказа по его идентификатору</summary>
        /// <param name="id">Идентификатор заказа</param>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDTO))]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _OrderService.GetOrderById(id);
            return Ok(order.ToDTO());
        }

        /// <summary>Создание заказа</summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <param name="OrderModel">Информация о заказе</param>
        /// <returns>Созданный заказ</returns>
        [HttpPost("{UserName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDTO))]
        public async Task<IActionResult> CreateOrder(string UserName, [FromBody] CreateOrderDTO OrderModel)
        {
            var order = await _OrderService.CreateOrder(UserName, OrderModel.Items.ToCartView(), OrderModel.Order);
            return Ok(order.ToDTO());
        }
    }
}
