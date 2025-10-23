using HFP.Application.Commands.Consumers;
using HFP.Application.DTO;
using HFP.Application.Queries.Consumers;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumersController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ConsumersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsumerCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ConsumerDto>>> GetAll([FromQuery] GetConsumersQuery query)
        {
            var data = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteConsumerCommand(id));
            return BaseOk();
        }
    }
}
