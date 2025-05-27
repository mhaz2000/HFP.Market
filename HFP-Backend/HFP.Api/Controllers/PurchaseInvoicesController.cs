using HFP.Application.Commands.Products;
using HFP.Application.Commands.PurchaseInvoice;
using HFP.Application.DTO;
using HFP.Application.Queries.Products;
using HFP.Application.Queries.PurchaseInvoices;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoicesController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public PurchaseInvoicesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseInvoiceCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromBody] UpdatePurchaseInvoiceCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeletePurchaseInvoiceCommand(id));
            return BaseOk();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EditPurchaseInvoiceDto>> Get([FromRoute] Guid id)
        {
            var data = await _queryDispatcher.QueryAsync(new GetPurchaseInvoiceQuery(id));
            return OkOrNotFound(data);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<PurchaseInvoiceDto>>> GetAll([FromQuery] GetPurchaseInvoicesQuery query)
        {
            var data = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(data);
        }
    }
}
