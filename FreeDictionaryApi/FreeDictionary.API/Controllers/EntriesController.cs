using FreeDictionary.Application.Business;
using FreeDictionary.Application.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Diagnostics;

namespace FreeDictionary.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EntriesController : Controller
    {
        private readonly IEntriesBusiness _entriesBusiness;
        public EntriesController(IEntriesBusiness entriesBusiness)
        {
            _entriesBusiness = entriesBusiness;
        }

        [HttpGet("{language}/Download")]
        public async Task<IActionResult> Download(string language)
        {
            var watcher = Stopwatch.StartNew();
            var (result, cache) = await _entriesBusiness.DownloadWordsAsync();
            watcher.Stop();
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());

            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpGet("{language}")]
        public async Task<IActionResult> Get(string language, string search, int page = 1, int limit = 10)
        {
            var watcher = Stopwatch.StartNew();
            var (result, cache) = await _entriesBusiness.GetAsync(search, page, limit);
            watcher.Stop();
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());
            return Ok(result);
        }

        [HttpGet("{language}/{word}")]
        public async Task<IActionResult> GetWord(string language, string word)
        {
            var watcher = Stopwatch.StartNew();
            var userId = User.Identity.GetUserId();
            var (result, cache) = await _entriesBusiness.GetByWordAsync(userId, word);
            watcher.Stop();
            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());

            return Ok(result);
        }

        [HttpPost("{language}/{word}/favorite")]
        public async Task<IActionResult> AddFavorite(string language, string word)
        {
            var watcher = Stopwatch.StartNew();
            var userId = User.Identity.GetUserId();
            var (result, cache) = await _entriesBusiness.AddFavoriteAsync(userId, word);
            watcher.Stop();

            if (!result)
                return BadRequest();

            HttpContext.Response.Headers.Add("x-cache", cache ? "HIT" : "MISS");
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());

            return NoContent();
        }

        [HttpDelete("{language}/{word}/unfavorite")]
        public async Task<IActionResult> RemoveFavorite(string language, string word)
        {
            var watcher = Stopwatch.StartNew();
            var userId = User.Identity.GetUserId();

            await _entriesBusiness.RemoveFavoriteAsync(userId, word);

            watcher.Stop();

            HttpContext.Response.Headers.Add("x-cache", "MISS");
            HttpContext.Response.Headers.Add("x-response-time", watcher.ElapsedMilliseconds.ToString());

            return NoContent();
        }
    }
}
