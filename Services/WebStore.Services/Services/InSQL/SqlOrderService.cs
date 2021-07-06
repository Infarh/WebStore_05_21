using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<SqlOrderService> _Logger;

        public SqlOrderService(WebStoreDB db, UserManager<User> UserManager, ILogger<SqlOrderService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .ThenInclude(item => item.Product)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync();

        public async Task<Order> GetOrderById(int id) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .ThenInclude(item => item.Product)
           .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} отсутствует в БД");

            _Logger.LogInformation("Формирование заказа для пользователя {0}", user);

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                User = user,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                Name = OrderModel.Name,
            };

            var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            var cart_products = await _db.Products
               .Where(p => product_ids.Contains(p.Id))
               .ToArrayAsync();

            order.Items = Cart.Items.Join(
                cart_products,
                cart_item => cart_item.Product.Id,
                cart_product => cart_product.Id,
                (cart_item, cart_product) => new OrderItem
                {
                    Order = order,
                    Product = cart_product,
                    Price = cart_product.Price, // здесь можно применить скидки...
                    Quantity = cart_item.Quantity,
                }).ToArray();

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            _Logger.LogInformation("Заказ для {0} успешно сформирован");

            return order;
        }
    }
}
