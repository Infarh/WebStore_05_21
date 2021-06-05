using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Staff")]
    //[Route("Employees")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesController> _Logger;

        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

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

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _EmployeesData.Get((int)id);
            if (employee is null)
                return NotFound();

            var view_model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };
            return View(view_model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            if (Model.LastName == "Qwe")
                ModelState.AddModelError("LastName", "Qwe - плохое имя!");

            if (Model.LastName == "Asd" && Model.Name == "Qwe")
                ModelState.AddModelError("", "Странное сочетание имени и фамилии");

            if (!ModelState.IsValid)
                return View(Model);

            _Logger.LogInformation("Редактирование сотрудника id:{0}", Model.Id);

            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.Name,
                Patronymic = Model.Patronymic,
                Age = Model.Age,
            };

            if (employee.Id == 0)
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Update(employee);

            _Logger.LogInformation("Редактирование сотрудника id:{0} завершено", Model.Id);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _EmployeesData.Get(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _Logger.LogInformation("Удаление сотрудника id:{0}", id);

            _EmployeesData.Delete(id);

            _Logger.LogInformation("Удаление сотрудника id:{0} завершено", id);
            return RedirectToAction("Index");
        }
    }
}
