using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    //[Route("Staff")]
    //[Route("Employees")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        //[Route("all")]
        public IActionResult Index() => View(_EmployeesData.GetAll());

        //[Route("info/{id}")]
        //[Route("info-id-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.Get(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }
    }
}
