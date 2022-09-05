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

        [HttpGet("Download")]
        public async Task<IActionResult> Download()
        {
            var result = await _entriesBusiness.DownloadWordsAsync();

            if (!result)
                return BadRequest();

            return NoContent();
        }

        [HttpGet("{language}/{word}")]
        public async Task<IActionResult> GetWord(string language, string word)
        {
            var userId = User.Identity.GetUserId();

            var meaning = await _entriesBusiness.GetByWordAsync(userId, word);

            return Ok(meaning);
        }
    }
}
