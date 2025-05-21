using HFP.Api.Hubs;
using HFP.Application.Commands.Interactive;
using HFP.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFP.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InteractiveController : ControllerBase
    {
        private readonly IHubContext<SystemHub> _hubContext;
        private readonly ICommandDispatcher _commandDispatcher;

        public InteractiveController(IHubContext<SystemHub> hubContext, ICommandDispatcher commandDispatcher)
        {
            _hubContext = hubContext;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("CustomerEntered")]
        public async Task<IActionResult> CustomerEntered([FromBody] CustomerEnteredCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);

            await _hubContext.Clients.All.SendAsync("ShowProductAnnouncement", new
            {
                title="خوش امدید",
                message="فروشگاه X، خوش آمدید. آیا نیاز به آموزش خرید محصول دارید؟"
            });

            return Ok(new { message = "triggered." });
        }
    }
}
