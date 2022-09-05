using FreeDictionary.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FreeDictionary.API.Controllers
{
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
    }
}
