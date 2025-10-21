using HFP.Application.Commands.Products;
using HFP.Application.Commands.Warehousemen;
using HFP.Application.DTO;
using HFP.Application.Queries.Products;
using HFP.Application.Queries.Warehousemen;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousemenController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public WarehousemenController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehousemanCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<WarehousemanDto>>> GetAll([FromQuery] GetWarehousemenQuery query)
        {
            var data = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteWarehousemanCommand(id));
            return BaseOk();
        }
    }
}
