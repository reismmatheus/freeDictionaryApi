using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeDictionary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(new { message = "Fullstack Challenge 🏅 - Dictionary" });
        }
    }
}
