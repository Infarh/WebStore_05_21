using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
