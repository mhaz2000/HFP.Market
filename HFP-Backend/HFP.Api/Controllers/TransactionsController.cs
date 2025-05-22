using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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

        [HttpGet("ProfitReport")]
        public async Task<ActionResult<PaginatedResult<ProfitReportDto>>> ProfitReport([FromQuery] GetProfitReportQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(result);
        }

        [HttpGet("ProfitReportExcel")]
        public async Task<IActionResult> ProfitReportExcel([FromQuery] GetProfitReportExcelQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return File(result.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transactions.xlsx");
        }

        [HttpGet("Excel")]
        public async Task<IActionResult> GetTransactionsExcel([FromQuery] GetTransactionsExcelQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return File(result.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transactions.xlsx");
        }


        [HttpGet("{transactionId}")]
        public async Task<ActionResult<IEnumerable<ProductTransactionDto>>> GetInvoice(Guid transactionId)
        {
            var result = await _queryDispatcher.QueryAsync(new GetTransactionQuery(transactionId));
            return OkOrNotFound(result);
        }
    }
}
