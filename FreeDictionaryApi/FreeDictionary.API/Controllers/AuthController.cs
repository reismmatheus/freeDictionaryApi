using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeDictionary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("Singup")]
        public ActionResult Singup()
        {
            return View();
        }
        [HttpPost("Singin")]
        public ActionResult Singin()
        {
            return View();
        }
    }
}
