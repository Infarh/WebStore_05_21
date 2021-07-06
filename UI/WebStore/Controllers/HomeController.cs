using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    //[Controller]
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

        public IActionResult Index([FromServices] IProductData ProductData)
        {
            ViewBag.Products = ProductData.GetProducts().Take(9).ToView();
            return View();
        }

        public IActionResult Throw(string Message) => throw new ApplicationException(Message ?? "Error in Main controller");

        public IActionResult SecondAction()
        {
            return Content(_Configuration["Greetings"]);
            //return View("Index");
        }

        public IActionResult Blog() => View();
        //public IActionResult BlogSingle() => View();
    }
}
