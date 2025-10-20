using HFP.Application.Commands.Products;
using HFP.Application.Commands.Transaction;
using HFP.Application.DTO;
using HFP.Application.Queries.Products;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Authorization;
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

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAll([FromQuery] GetProductsQuery query)
        {
            var data = await _queryDispatcher.QueryAsync(query);
            return BaseObjectOk(data);
        }

        [HttpGet("Excel")]
        public async Task<IActionResult> GetExcel([FromQuery] GetProductsExcelQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return File(result.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Transactions.xlsx");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid id)
        {
            var data = await _queryDispatcher.QueryAsync(new GetProductQuery(id));
            return OkOrNotFound(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteProductCommand(id));
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpPut("TakeProducts")]
        public async Task<IActionResult> TakeProducts([FromBody] TakeProductsCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [AllowAnonymous]
        [HttpPut("PutProducts")]
        public async Task<IActionResult> PutProducts([FromBody] PutProductsCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }
    }
}
