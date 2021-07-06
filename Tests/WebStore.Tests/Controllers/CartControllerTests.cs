using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using WebStore.Controllers;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public async Task CheckOut_ModelState_Invalid_Returns_View_with_Model()
        {
            const string expected_name = "Test order";

            var cart_service_mock = new Mock<ICartService>();
            var order_service_mock = new Mock<IOrderService>();

            var controller = new CartController(cart_service_mock.Object);
            controller.ModelState.AddModelError("error", "InvalidModel");

            var order_model = new OrderViewModel
            {
                Name = expected_name
            };

            var result = await controller.CheckOut(order_model, order_service_mock.Object);

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CartOrderViewModel>(view_result.Model);

            Assert.Equal(expected_name, model.Order.Name);
        }

        [TestMethod]
        public async Task CheckOut_ModelState_Valid_Call_Service_and_Returns_Redirect()
        {
            var cart_service_mock = new Mock<ICartService>();
            cart_service_mock
               .Setup(c => c.GetViewModel())
               .Returns(new CartViewModel
               {
                   Items = new[] { (new ProductViewModel { Name = "Test product" }, 1) }
               });

            const int expected_order_id = 1;
            const string expected_order_name = "Test order";
            const string expected_order_address = "Test address";
            const string expected_order_phone = "123";
            var order_service_mock = new Mock<IOrderService>();
            order_service_mock
               .Setup(c => c.CreateOrder(It.IsAny<string>(), It.IsAny<CartViewModel>(), It.IsAny<OrderViewModel>()))
               .ReturnsAsync(
                    new Order
                    {
                        Id = expected_order_id,
                        Name = expected_order_name,
                        Address = expected_order_address,
                        Phone = expected_order_phone,
                        Date = DateTime.Now,
                        Items = Array.Empty<OrderItem>()
                    });

            const string expected_user = "Test user";
            var controller = new CartController(cart_service_mock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, expected_user) }))
                    }
                }
            };

            var order_model = new OrderViewModel
            {
                Name = expected_order_name,
                Address = expected_order_address,
                Phone = expected_order_phone
            };

            var result = await controller.CheckOut(order_model, order_service_mock.Object);

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirect_result.ActionName);
            Assert.Null(redirect_result.ControllerName);

            Assert.Equal(expected_order_id, redirect_result.RouteValues["id"]);
        }
    }
}
