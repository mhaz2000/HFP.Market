using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Products
{
    public record GetProductsQuery : PaginationQuery, IQuery<PaginatedResult<ProductDto>>;
}
