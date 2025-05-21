using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Discounts
{
    public record GetDiscountsQuery : PaginationQuery, IQuery<PaginatedResult<DiscountDto>>;

}
