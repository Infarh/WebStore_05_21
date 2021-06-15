using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployesData> _Logger;
        private int _CurrentMaxId;

        public InMemoryEmployesData(ILogger<InMemoryEmployesData> Logger)
        {
            _Logger = Logger;
            _CurrentMaxId = TestData.Employees.Max(i => i.Id);
        }

        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public Employee Get(int id) => TestData.Employees.SingleOrDefault(employee => employee.Id == id);

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee)) return employee.Id; // характерно только если inMemory!!!! Для БД не нужно!

            employee.Id = ++_CurrentMaxId;
            TestData.Employees.Add(employee);

            _Logger.LogInformation("Сотрудник id:{0} добавлен", employee.Id);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee)) return; // Тоже только для реализации на List<T>... Для БД не нужно!

            var db_item = Get(employee.Id);
            if (db_item is null) return;

            //db_item.Id = employee.Id; // не делаем, так как во-первых нельзя редактировать id, а во вторых здесь(!) это бессмысленно!
            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;

            _Logger.LogInformation("Сотрудник id:{0} отредактирован", employee.Id);
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null)
            {
                _Logger.LogWarning("При удалении сотрудник id:{0} не найден", id);
                return false;
            }

            var result = TestData.Employees.Remove(db_item);

            if (result)
                _Logger.LogInformation("Сотрудник id:{0} удалён", id);
            else
                _Logger.LogError("При удалении сотрудник id:{0} не найден", id);

            return result;
        }
    }
}
