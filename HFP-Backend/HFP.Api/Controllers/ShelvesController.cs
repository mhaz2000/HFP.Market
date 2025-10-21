using HFP.Application.Commands.Products;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ShelvesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }
    }
}
