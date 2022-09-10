using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeDictionary.API.Controllers
{
    [ApiController]
    [Route("")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { message = "Fullstack Challenge 🏅 - Dictionary" });
        }
    }
}
