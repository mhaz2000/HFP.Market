using HFP.Application.DTO;
using HFP.Application.Queries.Dashboard;
using HFP.Application.Queries.Products;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public DashboardController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDto>> Get([FromQuery] GetDashboardQuery query)
        {
            var data = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(data);
        }
    }
}
