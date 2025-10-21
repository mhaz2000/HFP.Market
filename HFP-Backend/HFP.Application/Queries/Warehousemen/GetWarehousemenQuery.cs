using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Warehousemen;
public record GetWarehousemenQuery : PaginationQuery, IQuery<PaginatedResult<WarehousemanDto>>;
