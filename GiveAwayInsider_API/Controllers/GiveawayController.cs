using GiveAwayInsider_API.Service.IService;
using GiveAwayInsider_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiveAwayInsider_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiveawayController : Controller
    {
        private readonly IGiveawayService _giveawayService;
        private readonly IEmailService _emailService;

        public GiveawayController(IGiveawayService giveawayService, IEmailService emailService)
        {
            _giveawayService = giveawayService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGiveaways([FromQuery(Name = "platform")] string? platform, [FromQuery(Name = "type")] string? type, [FromQuery(Name = "sortBy")] string? sortBy)
        {
            var result = await _giveawayService.GetGiveaways(platform, type, sortBy);
            if (result.IsSuccess)
                return Ok(result.Data as IEnumerable<GiveawayDTO>);
            else if (result.StatusCode == 404)
                return NotFound();
            else 
                return BadRequest();
        }

        [Authorize(AuthenticationSchemes = "ApiKey")]
        [HttpPut("notify/{id:int}")]
        public async Task<IActionResult> NotifyGiveaway(int id, [FromBody] IEnumerable<GiveawayDTO> giveaways)
        {
            var result = await _emailService.SendNotification(giveaways, id);
            if (result.IsSuccess)
                return Ok();
            else
                return BadRequest();
        }
    }
}
