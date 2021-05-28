using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebStore.Infrastructure.MiddleWare
{
    public class TestMiddleWare
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<TestMiddleWare> _Logger;

        public TestMiddleWare(RequestDelegate next, ILogger<TestMiddleWare> Logger)
        {
            _Next = next;
            _Logger = Logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //Обработка контекста
            var processing = _Next(context);

            // Выполнять работу в то время, пока оставшаяся часть конвейера что-то там делает с контекстом...

            await processing;
            // Обработать результат работы оставшейся части конвейера
        }
    }
}
