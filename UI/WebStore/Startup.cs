using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.MiddleWare;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestAPI;
using WebStore.Services.Data;
using WebStore.Services.Services.InCookies;
using WebStore.Services.Services.InMemory;
using WebStore.Services.Services.InSQL;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Values;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //var connection_string = new SqlConnectionStringBuilder(Configuration.GetConnectionString("MSSQL"))
            //{
            //    UserID = "qwe",
            //    Password = "asd"
            //};
            //var connection_string_with_password = connection_string.ConnectionString;

            var database_name = Configuration["Database"];

            switch (database_name)
            {
                case "MSSQL":
                    services.AddDbContext<WebStoreDB>(opt =>
                        opt.UseSqlServer(
                            Configuration.GetConnectionString("MSSQL")//,
                            /*o => o.MigrationsAssembly("WebStore.DAL.SqlServer")*/));
                    break;
                case "Sqlite":
                    services.AddDbContext<WebStoreDB>(opt =>
                        opt.UseSqlite(
                            Configuration.GetConnectionString("Sqlite"),
                            o => o.MigrationsAssembly("WebStore.DAL.Sqlite")));
                    break;
            }

            services.AddTransient<WebStoreDBInitializer>();

            services.AddIdentity<User, Role>(/*opt => { }*/)
               .AddEntityFrameworkStores<WebStoreDB>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif

                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            //services.AddSingleton<IEmployeesData, InMemoryEmployesData>();  // Объект InMemoryEmployesData создаётся один раз на всё время работы приложения
            //services.AddScoped<IEmployeesData, SqlEmployeesData>();
            services.AddScoped<ICartService, InCookiesCartService>();
            //services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IOrderService, SqlOrderService>();

            services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));
            services.AddHttpClient<IEmployeesData, EmployeesClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));
            services.AddHttpClient<IProductData, ProductsClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));

            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllersConvention()))
               .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            //var initializer = services.GetRequiredService<WebStoreDBInitializer>();
            //initializer.Initialize();
            using (var scope = services.CreateScope())
                scope.ServiceProvider.GetRequiredService<WebStoreDBInitializer>().Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TestMiddleWare>();

            app.UseWelcomePage("/WelcomePage");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
