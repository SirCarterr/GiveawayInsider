using GiveAwayInsider_Business.Repository.IRepository;
using GiveAwayInsider_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiveAwayInsider_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsController(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        [HttpGet("{userId}")]
        [ActionName("get")]
        public async Task<IActionResult> Get(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var settings = await _settingsRepository.GetSettings(userId);

            if (settings == null)
                return NotFound();
            
            return Ok(settings);
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<IActionResult> Update([FromBody] SettingsDTO settingsDTO)
        {
            var settings = await _settingsRepository.UpdateSettings(settingsDTO);
            return Ok(settings);
        }
    }
}
