using FreeDictionary.Application.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _entriesBusiness.DownloadWordsAsync();

            if (!result)
                return BadRequest();

            return NoContent();
        }
        [HttpGet("{language}")]
        public async Task<IActionResult> Get(string language, string search, int page = 1, int limit = 10)
        {
            var words = await _entriesBusiness.GetAsync(search, page, limit);
            return Ok(words);
        }

        [HttpGet("{language}/{word}")]
        public async Task<IActionResult> GetWord(string language, string word)
        {
            var userId = User.Identity.GetUserId();

            var meaning = await _entriesBusiness.GetByWordAsync(userId, word);

            return Ok(meaning);
        }

        [HttpPost("{language}/{word}/favorite")]
        public async Task<IActionResult> AddFavorite(string language, string word)
        {
            var userId = User.Identity.GetUserId();

            var addFavorite = await _entriesBusiness.AddFavoriteAsync(userId, word);

            if (!addFavorite)
                return BadRequest();

            return NoContent();
        }

        [HttpPost("{language}/{word}/unfavorite")]
        public async Task<IActionResult> RemoveFavorite(string language, string word)
        {
            var userId = User.Identity.GetUserId();

            await _entriesBusiness.RemoveFavoriteAsync(userId, word);

            return NoContent();
        }
    }
}
