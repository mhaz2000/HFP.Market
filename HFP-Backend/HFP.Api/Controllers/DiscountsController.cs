using HFP.Application.Commands.Discounts;
using HFP.Application.DTO;
using HFP.Application.Queries.Discounts;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public DiscountsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiscountCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDiscountCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpPut("Apply")]
        public async Task<ActionResult<AppliedDiscountDto>> Apply([FromBody] ApplyDiscountCommand command)
        {
            var result = await _commandDispatcher.DispatchAsync<ApplyDiscountCommand ,AppliedDiscountDto>(command);
            return OkOrNotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<DiscountDto>>> GetAll([FromQuery] GetDiscountsQuery query)
        {
            var data = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDto>> Get([FromRoute] Guid id)
        {
            var data = await _queryDispatcher.QueryAsync(new GetDiscountQuery(id));
            return OkOrNotFound(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteDiscountCommand(id));
            return BaseOk();
        }


    }
}
