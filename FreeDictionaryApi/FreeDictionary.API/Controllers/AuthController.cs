using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            var result = await _authBusiness.Singup(model);

            if (result == null)
                return BadRequest(new { message = "Error message" });

            return Ok(result);
        }

        [HttpPost("Singin")]
        public async Task<IActionResult> Singin(SinginModel model)
        {
            var result = await _authBusiness.Singin(model);

            if(result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
