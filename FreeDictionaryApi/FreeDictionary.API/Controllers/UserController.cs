using FreeDictionary.Application.Interface;
using Microsoft.AspNet.Identity;
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
            var userId = User.Identity.GetUserId();
            var profile = await _userBusiness.GetProfile(userId);
            return Ok(profile);
        }

        [HttpGet("Me/History")]
        public async Task<IActionResult> GetHistory()
        {
            var userId = User.Identity.GetUserId();
            var history = await _userBusiness.GetHistory(userId);
            return Ok(history);
        }

        [HttpGet("Me/Favorities")]
        public async Task<IActionResult> GetFavorities()
        {
            var userId = User.Identity.GetUserId();
            var favorities = await _userBusiness.GetFavorities(userId);
            return Ok(favorities);
        }
    }
}
