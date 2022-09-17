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
            HttpContext.Response.Headers.Add("x-cache", "MISS");
            return Ok(new { message = "Fullstack Challenge 🏅 - Dictionary" });
        }
    }
}
