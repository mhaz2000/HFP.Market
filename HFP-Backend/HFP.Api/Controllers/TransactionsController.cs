using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public TransactionsController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<TransactionDto>>> GetTransactions([FromQuery] GetTransactionsQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(result);
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<IEnumerable<ProductTransactionDto>>> GetInvoice(Guid transactionId)
        {
            var result = await _queryDispatcher.QueryAsync(new GetTransactionQuery(transactionId));
            return OkOrNotFound(result);
        }
    }
}
