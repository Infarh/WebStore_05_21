using System;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        [HttpGet("clear")]
        public void Clear() => Console.Clear();

        [HttpGet("WriteLine")]
        public void WriteLine(string Message) => Console.WriteLine(Message);
    }
}
