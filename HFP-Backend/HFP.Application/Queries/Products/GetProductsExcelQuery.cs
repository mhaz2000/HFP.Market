using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Products
{
    public record GetProductsExcelQuery : PaginationQuery, IQuery<MemoryStream>;
}
