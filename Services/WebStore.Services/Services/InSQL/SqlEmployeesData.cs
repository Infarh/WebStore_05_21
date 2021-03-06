using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<SqlEmployeesData> _Logger;

        public SqlEmployeesData(WebStoreDB db, ILogger<SqlEmployeesData> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public IEnumerable<Employee> GetAll() => _db.Employees.ToArray();

        //public Employee Get(int id) => _db.Employees.Find(id);
        //public Employee Get(int id) => _db.Employees.FirstOrDefault(employee => employee.Id == id);
        public Employee Get(int id) => _db.Employees.SingleOrDefault(employee => employee.Id == id);

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            //_db.Employees.Add(employee);
            //_db.Entry(employee).State = EntityState.Added;
            _db.Add(employee);

            _db.SaveChanges();

            _Logger.LogInformation("Сотрудник {0} добавлен", employee);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            //_db.Employees.Update(employee);
            //_db.Entry(employee).State = EntityState.Modified;
            _db.Update(employee);
            _Logger.LogInformation("Сотрудник id:{0} отредактирован", employee);

            _db.SaveChanges();
        }

        public bool Delete(int id)
        {
            //_db.Database.ExecuteSqlRaw("DELETE ...");

            var employee = _db.Employees
               .Select(e => new Employee { Id = e.Id })
               .FirstOrDefault(e => e.Id == id);
            if (employee is null) return false;
            //if (Get(id) is not { } employee) return false;

            //_db.Employees.Remove(employee);
            //_db.Entry(employee).State = EntityState.Deleted;
            _db.Remove(employee);

            _db.SaveChanges();

            _Logger.LogInformation("Сотрудник id:{0} удалён", id);

            return true;
        }
    }
}
