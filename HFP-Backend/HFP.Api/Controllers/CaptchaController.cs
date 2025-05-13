using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HFP.Application.DTO;
using HFP.Shared.Abstractions.Commands;
using HFP.Application.Commands.Captcha;

namespace HFP.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CaptchaController(ICommandDispatcher commandDispatcher)
            => _commandDispatcher = commandDispatcher;
        
        [HttpGet]
        public async Task<ActionResult<CaptchaDto>> Get()
        {
            var result = await _commandDispatcher.DispatchAsync<GetCatpchaCommand, CaptchaDto>(new());
            return OkOrNotFound(result);
        }
    }
}
