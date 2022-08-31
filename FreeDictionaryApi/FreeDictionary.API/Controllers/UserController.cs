using FreeDictionary.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeDictionary.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [HttpGet("Me")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _userBusiness.GetProfile();
            return View();
        }

        [HttpGet("Me/History")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _userBusiness.GetHistory();
            return View();
        }

        [HttpGet("Me/Favorities")]
        public async Task<IActionResult> GetFavorities()
        {
            var favorities = await _userBusiness.GetFavorities();
            return View();
        }
    }
}
