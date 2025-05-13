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

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddProductToCartCommand command)
        {
            var result = await _commandDispatcher.DispatchAsync<AddProductToCartCommand, bool>(command);
            if(result)
                await _hubContext.Clients.All.SendAsync("ShowInvoice", new
                {
                    data = command.BuyerId
                });

            return BaseOk();
        }

        [HttpGet("{buyerId}")]
        public async Task<ActionResult<IEnumerable<ProductTransactionDto>>> GetInvoice(string buyerId)
        {
            var result = await _queryDispatcher.QueryAsync(new GetInvoiceQuery(buyerId));
            return OkOrNotFound(result);
        }
    }
}
