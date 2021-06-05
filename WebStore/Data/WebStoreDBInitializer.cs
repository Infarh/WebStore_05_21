using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDBInitializer> _Logger;

        public WebStoreDBInitializer(WebStoreDB db, ILogger<WebStoreDBInitializer> Logger)
        {
            _db = db;
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

            _Logger.LogInformation("Инициализация БД завершена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
            {
                _Logger.LogInformation("БД не нуждается в инициализации товаров");
                return;
            }

            _Logger.LogInformation("Инициализация секций...");
            var timer = Stopwatch.StartNew();


            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON"); // Костыль!!!
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Инициализация секций выполнена. {0} c", timer.Elapsed.TotalSeconds);

            _Logger.LogInformation("Инициализация брендов...");

            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON"); // Костыль!!!
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Инициализация брендов выполнена за. {0} c", timer.Elapsed.TotalSeconds);

            _Logger.LogInformation("Инициализация товаров...");

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON"); // Костыль!!!
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Инициализация товаров выполнена за. {0} c", timer.Elapsed.TotalSeconds);
        }
    }
}
