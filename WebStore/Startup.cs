using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.MiddleWare;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<ITestService, TestService>();
            //services.AddScoped<IPrinter, DebugPrinter>();

            services.AddSingleton<IEmployeesData, InMemoryEmployesData>();  // Объект InMemoryEmployesData создаётся один раз на всё время работы приложения
            // Нужен если сервис должен хранить состояние на время работы приложения

            //services.AddScoped<IEmployeesData, InMemoryEmployesData>();     // Объект создаётся единожды для области
            // Если нужен сервис, который обладает памятью только в пределах обработки одного запроса

            //services.AddTransient<IEmployeesData, InMemoryEmployesData>();  // Объект InMemoryEmployesData создаётся каждый раз заново
            // Когда сервис не подразумевает наличие внутренней памяти

            services.AddSingleton<IProductData, InMemoryProductData>();

            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllersConvention()))
               .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            //var test_service = services.GetRequiredService<ITestService>();
            //test_service.Test();


            //var employees1 = services.GetService<IEmployeesData>();
            //var employees2 = services.GetService<IEmployeesData>();

            //IEmployeesData employyes3;

            //using (var scope = services.CreateScope())
            //    employyes3 = scope.ServiceProvider.GetService<IEmployeesData>();

            //var is_equals = ReferenceEquals(employees1, employyes3);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<TestMiddleWare>();

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //});

            //app.Map("/TestMapRequest", opt => opt.Run(async context =>
            //{
            //    await Task.Delay(100);
            //    var stream_writer = new StreamWriter(context.Response.Body);
            //    await stream_writer.WriteAsync("Hello from TestMapRequest");
            //    await context.Response.CompleteAsync();
            //}));

            app.UseWelcomePage("/WelcomePage");

            //var greetings = Configuration["Greetings"];
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    //await context.Response.WriteAsync(greetings);
                    await context.Response.WriteAsync(Configuration["Greetings"]);
                });

                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    interface ITestService
    {
        void Test();
    }


    class TestService : ITestService
    {
        private IPrinter _Printer;

        public TestService(IPrinter Printer)
        {
            _Printer = Printer;
        }

        public void Test()
        {
            _Printer.Print("Запуск теста");
        }
    }

    interface IPrinter
    {
        void Print(string str);
    }

    class DebugPrinter : IPrinter
    {
        public DebugPrinter()
        {
            
        }

        public void Print(string str)
        {
            Debug.WriteLine(str);
        }
    }
}
