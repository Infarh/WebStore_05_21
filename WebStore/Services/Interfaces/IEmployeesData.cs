using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee Get(int id);

        int Add(Employee employee);

        void Update(Employee employee);

        bool Delete(int id);
    }
}
