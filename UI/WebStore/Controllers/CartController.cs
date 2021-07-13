using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService) => _CartService = CartService;

        public IActionResult Index() => View(new CartOrderViewModel { Cart = _CartService.GetViewModel() });

        public IActionResult Add(int id)
        {
            _CartService.Add(id);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Remove(int id)
        {
            _CartService.Remove(id);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Decrement(int id)
        {
            _CartService.Decrement(id);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Clear()
        {
            _CartService.Clear();
            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _CartService.GetViewModel(),
                    Order = OrderModel
                });

            var order = await OrderService.CreateOrder(
                User.Identity!.Name,
                _CartService.GetViewModel(),
                OrderModel)
               .ConfigureAwait(true);

            _CartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region Web-API

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddAPI(int id)
        {
            _CartService.Add(id);
            return Json(new { id, message = $"Товар с id {id} был добавлен в корзину" });
        }

        public IActionResult RemoveAPI(int id)
        {
            _CartService.Remove(id);
            return Ok(new { id, message = $"Товар с id {id} был удалён из корзины" });
        }

        public IActionResult DecrementAPI(int id)
        {
            _CartService.Decrement(id);
            return Ok();
        }

        public IActionResult ClearAPI()
        {
            _CartService.Clear();
            return Ok();
        }

        #endregion
    }
}
