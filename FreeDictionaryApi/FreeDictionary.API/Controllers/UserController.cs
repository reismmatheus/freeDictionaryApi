using FreeDictionary.Application.Business;
using FreeDictionary.Application.Interface;
using FreeDictionary.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            var watcher = Stopwatch.StartNew();

            var userId = User.Identity.GetUserId();
            var profile = await _userBusiness.GetProfileAsync(userId);

            watcher.Stop();
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());
            return Ok(profile);
        }

        [HttpGet("Me/History")]
        public async Task<IActionResult> GetHistory(int page = 1, int limit = 10)
        {
            var watcher = Stopwatch.StartNew();

            var userId = User.Identity.GetUserId();
            var history = await _userBusiness.GetHistoryAsync(userId, page, limit);

            watcher.Stop();
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());
            return Ok(history);
        }

        [HttpGet("Me/Favorites")]
        public async Task<IActionResult> GetFavorites(int page = 1, int limit = 10)
        {
            var watcher = Stopwatch.StartNew();

            var userId = User.Identity.GetUserId();
            var favorities = await _userBusiness.GetFavoritesAsync(userId, page, limit);

            watcher.Stop();
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());
            return Ok(favorities);
        }
    }
}
