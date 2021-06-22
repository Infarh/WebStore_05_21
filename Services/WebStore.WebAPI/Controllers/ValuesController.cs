using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private static List<string> __Values = Enumerable
           .Range(1, 10)
           .Select(i => $"Value-{i}")
           .ToList();

        //[HttpGet] public IEnumerable<string> Get() => __Values;
        //[HttpGet] public ActionResult<List<string>> Get() => __Values;

        [HttpGet]
        public IActionResult Get() => Ok(__Values);

        [HttpGet("count")]
        public IActionResult GetCount() => Ok(__Values.Count);

        [HttpGet("{index}")]
        [HttpGet("index[[{index}]]")]
        public ActionResult<string> GetByIndex(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index >= __Values.Count)
                return NotFound();

            return __Values[index];
        }

        [HttpPost]
        [HttpPost("add")] // POST -> http://localhost:5001/api/values/add
        public ActionResult Add(string str)
        {
            __Values.Add(str);
            return Ok();
            //return CreatedAtAction(nameof(GetByIndex), new { index = __Values.Count - 1});
        }

        [HttpPut("{index}")]
        [HttpPut("edit/{index}")]
        public ActionResult Replace(int index, string NewStr)
        {
            if (index < 0)
                return BadRequest();
            if (index >= __Values.Count)
                return NotFound();

            __Values[index] = NewStr;
            return Ok();
        }

        [HttpDelete("{index}")]
        public ActionResult Delete(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index >= __Values.Count)
                return NotFound();

            __Values.RemoveAt(index);

            return Ok();
        }
    }
}
