using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Products
{
    public record GetProductQuery(Guid Id) : IQuery<ProductDto>;
}
