using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO
{
    /// <summary>Заказ</summary>
    public class OrderDTO
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Название</summary>
        public string Name { get; set; }

        /// <summary>Телефонный номер для связи</summary>
        public string Phone { get; set; }

        /// <summary>Адрес доставки</summary>
        public string Address { get; set; }

        /// <summary>Дата формирования</summary>
        public DateTime Date { get; set; }

        /// <summary>Пункты заказа</summary>
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    /// <summary>Пункты заказа</summary>
    public class OrderItemDTO
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор товара</summary>
        public int ProductId { get; set; }

        /// <summary>Цена</summary>
        public decimal Price { get; set; }

        /// <summary>Количество</summary>
        public int Quantity { get; set; }
    }

    /// <summary>Информация о создаваемом заказе</summary>
    public class CreateOrderDTO
    {
        /// <summary>Модель заказа</summary>
        public OrderViewModel Order { get; set; }

        /// <summary>Пункты заказа</summary>
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem Item) => Item is null
            ? null
            : new OrderItemDTO
            {
                Id = Item.Id,
                ProductId = Item.Product.Id,
                Price = Item.Price,
                Quantity = Item.Quantity,
            };

        public static OrderItem FromDTO(this OrderItemDTO Item) => Item is null
            ? null
            : new OrderItem
            {
                Id = Item.Id,
                Product = new Product { Id = Item.Id },
                Price = Item.Price,
                Quantity = Item.Quantity,
            };

        public static OrderDTO ToDTO(this Order Order) => Order is null
            ? null
            : new OrderDTO
            {
                Id = Order.Id,
                Name = Order.Name,
                Address = Order.Address,
                Phone = Order.Phone,
                Date = Order.Date,
                Items = Order.Items.Select(ToDTO)
            };

        public static Order FromDTO(this OrderDTO Order) => Order is null
            ? null
            : new Order
            {
                Id = Order.Id,
                Name = Order.Name,
                Address = Order.Address,
                Phone = Order.Phone,
                Date = Order.Date,
                Items = Order.Items.Select(FromDTO).ToList()
            };

        public static IEnumerable<OrderDTO> ToDTO(this IEnumerable<Order> Orders) => Orders.Select(ToDTO);
        public static IEnumerable<Order> FromDTO(this IEnumerable<OrderDTO> Orders) => Orders.Select(FromDTO);

        public static IEnumerable<OrderItemDTO> ToDTO(this CartViewModel Cart) =>
            Cart.Items.Select(p => new OrderItemDTO
            {
                ProductId = p.Product.Id,
                Price = p.Product.Price,
                Quantity = p.Quantity
            });

        public static CartViewModel ToCartView(this IEnumerable<OrderItemDTO> Items) =>
            new()
            {
                Items = Items.Select(p => (new ProductViewModel { Id = p.ProductId }, p.Quantity))
            };
    }
}
