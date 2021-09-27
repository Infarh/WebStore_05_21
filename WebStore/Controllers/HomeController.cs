using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Controller]
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

        public IActionResult Index() => View();

        public IActionResult SecondAction()
        {
            return Content(_Configuration["Greetings"]);
            //return View("Index");
        }

        public IActionResult Blog() => View();
        //public IActionResult BlogSingle() => View();

        [Route("~/Status/{Code}")]
        public IActionResult Status(string Code) => Content($"Status - {Code}");
    }
}
