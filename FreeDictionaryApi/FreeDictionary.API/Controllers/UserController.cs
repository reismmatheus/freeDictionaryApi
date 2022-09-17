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
            var userId = User.Identity.GetUserId();
            var (result, cache) = await _userBusiness.GetProfileAsync(userId);
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");

            return Ok(result);
        }

        [HttpGet("Me/History")]
        public async Task<IActionResult> GetHistory(int page = 1, int limit = 10)
        {
            var userId = User.Identity.GetUserId();
            var (result, cache) = await _userBusiness.GetHistoryAsync(userId, page, limit);
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");
            return Ok(result);
        }

        [HttpGet("Me/Favorites")]
        public async Task<IActionResult> GetFavorites(int page = 1, int limit = 10)
        {
            var userId = User.Identity.GetUserId();
            var (result, cache) = await _userBusiness.GetFavoritesAsync(userId, page, limit);
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");
            return Ok(result);
        }
    }
}
