using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Role.Administrators)]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
