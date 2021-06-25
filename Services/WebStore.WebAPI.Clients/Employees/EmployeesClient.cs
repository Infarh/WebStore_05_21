using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient Client) : base(Client, WebAPIAddress.Employees) { }

        public IEnumerable<Employee> GetAll()
        {
            return Get<IEnumerable<Employee>>(Address);
        }

        public Employee Get(int id)
        {
            return Get<Employee>($"{Address}/{id}");
        }

        public int Add(Employee employee)
        {
            var response = Post(Address, employee);
            var id = response.Content.ReadFromJsonAsync<int>().Result;
            return id;
        }

        public void Update(Employee employee)
        {
            Put(Address, employee);
        }

        public bool Delete(int id)
        {
            var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
            return result;
        }
    }
}
