using FreeDictionary.CrossCutting.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace FreeDictionary.API.Controllers
{
    [ApiController]
    [Route("")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var watcher = Stopwatch.StartNew();
            watcher.Stop();
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());
            return Ok(new { message = "Fullstack Challenge 🏅 - Dictionary" });
        }
    }
}
