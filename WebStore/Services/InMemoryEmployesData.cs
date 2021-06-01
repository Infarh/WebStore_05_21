using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services
{
    public class InMemoryEmployesData : IEmployeesData
    {
        private int _CurrentMaxId;

        public InMemoryEmployesData()
        {
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

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if(TestData.Employees.Contains(employee)) return; // Тоже только для реализации на List<T>... Для БД не нужно!

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
            return TestData.Employees.Remove(db_item);
        }
    }
}
