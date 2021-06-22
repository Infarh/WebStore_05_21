using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<WebStoreDBInitializer> _Logger;

        public WebStoreDBInitializer(
            WebStoreDB db,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WebStoreDBInitializer> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _Logger = Logger;
        }

        public void Initialize()
        {
            _Logger.LogInformation("Инициализация БД...");
            var timer = Stopwatch.StartNew();

            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _Logger.LogInformation("Миграция БД...");
                _db.Database.Migrate();
                _Logger.LogInformation("Миграция БД выполнена за {0}c", timer.Elapsed.TotalSeconds);
            }
            else
                _Logger.LogInformation("Миграция БД не требуется. {0}c", timer.Elapsed.TotalSeconds);

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Ошибка при инициализации товаров в БД");
                throw;
            }

            try
            {
                //InitializeIdentityAsync().Wait();
                InitializeIdentityAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Ошибка при инициализации данных БД системы Identity");
                throw;
            }

            try
            {
                AddEmployees();
            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Ошибка при инициализации данных БД сотрудников");
                throw;
            }

            _Logger.LogInformation("Инициализация БД завершена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _Logger.LogInformation("БД не нуждается в инициализации товаров");
                return;
            }
            var timer = Stopwatch.StartNew();


            var sections_pool = TestData.Sections.ToDictionary(section => section.Id);
            var brands_pool = TestData.Brands.ToDictionary(brand => brand.Id);

            foreach (var section in TestData.Sections.Where(s => s.ParentId != null))
                section.Parent = sections_pool[(int)section.ParentId!];

            foreach (var product in TestData.Products)
            {
                product.Section = sections_pool[product.SectionId];
                if (product.BrandId is { } brand_id)
                    product.Brand = brands_pool[brand_id];

                product.Id = 0;
                product.SectionId = 0;
                product.BrandId = null;
            }

            foreach (var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;


            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);
                _db.Products.AddRange(TestData.Products);

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Инициализация товаров выполнена за. {0} c", timer.Elapsed.TotalSeconds);
        }

        private async Task InitializeIdentityAsync()
        {
            _Logger.LogInformation("Инициализация БД системы Identity");
            var timer = Stopwatch.StartNew();

            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Роль {0} отсутствует. Создаю...", RoleName);
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
                    _Logger.LogInformation("Роль {0} создана успешно", RoleName);
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("Пользователь {0} отсутствует. Создаю...", User.Administrator);

                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} успешно создан", User.Administrator);

                    await _UserManager.AddToRoleAsync(admin, Role.Administrators);

                    _Logger.LogInformation("Пользователь {0} наделён ролью {1}", 
                        User.Administrator, Role.Administrators);
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description).ToArray();
                    _Logger.LogError("Учётная запись администратора не создана по причине: {0}", 
                        string.Join(",", errors));

                    throw new InvalidOperationException($"Ошибка при создании пользователя {User.Administrator}:{string.Join(",", errors)}");
                }
            }

            _Logger.LogInformation("Инициализация данных БД системы Identity выполнена за {0} c",
                timer.Elapsed.TotalSeconds);
        }

        private void AddEmployees()
        {
            if(_db.Employees.Any()) return;

            TestData.Employees.ForEach(e => e.Id = 0);
            _db.Employees.AddRange(TestData.Employees);
            _db.SaveChanges();
        }
    }
}
