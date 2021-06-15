using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewModels;

namespace WebStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrder(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);
    }
}
