using HFP.Api.Hubs;
using HFP.Application.Commands.Purchase;
using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFP.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IHubContext<SystemHub> _hubContext;

        public PurchaseController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IHubContext<SystemHub> hubContext)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _hubContext = hubContext;
        }

        [HttpPost("{code}")]
        public async Task<IActionResult> AddToCart([FromRoute] string code)
        {
            var result = await _commandDispatcher.DispatchAsync<AddProductToCartCommand, string?>(new AddProductToCartCommand(code));
            if (result is not null)
                await _hubContext.Clients.All.SendAsync("ShowInvoice", new
                {
                    data = result
                });

            return BaseOk();
        }

        [HttpPut]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveProductFromCartCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return BaseOk();
        }

        [HttpGet("{buyerId}")]
        public async Task<ActionResult<IEnumerable<ProductTransactionDto>>> GetInvoice(string buyerId)
        {
            var result = await _queryDispatcher.QueryAsync(new GetInvoiceQuery(buyerId));
            return OkOrNotFound(result);
        }

        [HttpPost("Payment/{buyerId}")]
        public async Task<ActionResult<PaymentResultDto>> Payment([FromRoute] string buyerId)
        {
            var result = await _commandDispatcher.DispatchAsync<PaymentCommand, PaymentResultDto>(new PaymentCommand(buyerId));
            return OkOrNotFound(result);
        }
    }
}
