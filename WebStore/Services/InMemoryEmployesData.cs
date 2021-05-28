using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryEmployesData : IEmployeesData
    {
        private readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 27 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 31 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18 },
        };

        private int _CurrentMaxId;

        public InMemoryEmployesData()
        {
            _CurrentMaxId = _Employees.Max(i => i.Id);
        }

        public IEnumerable<Employee> GetAll() => _Employees;

        public Employee Get(int id) => _Employees.SingleOrDefault(employee => employee.Id == id);

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return employee.Id; // характерно только если inMemory!!!! Для БД не нужно!

            employee.Id = ++_CurrentMaxId;
            _Employees.Add(employee);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if(_Employees.Contains(employee)) return; // Тоже только для реализации на List<T>... Для БД не нужно!

            var db_item = Get(employee.Id);
            if(db_item is null) return;

            //db_item.Id = employee.Id; // не делаем, так как во-первых нельзя резактировать id, а во вторых здесь(!) это бессмысленно!
            db_item.LastName = employee.LastName;
            db_item.FirstName = employee.FirstName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Age = employee.Age;

        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null) return false;
            return _Employees.Remove(db_item);
        }
    }
}
