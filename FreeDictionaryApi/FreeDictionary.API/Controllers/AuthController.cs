using FreeDictionary.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FreeDictionary.Application.Model.AuthModel;

namespace FreeDictionary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("Singup")]
        public async Task<IActionResult> Singup(SingupModel model)
        {
            var user = await _authBusiness.Singup(model);
            return Ok();
        }
        [HttpPost("Singin")]
        public async Task<IActionResult> Singin(SinginModel model)
        {
            var user = await _authBusiness.Singim(model);
            return View();
        }
    }
}
